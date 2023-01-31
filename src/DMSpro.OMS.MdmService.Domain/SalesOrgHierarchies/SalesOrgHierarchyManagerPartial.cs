using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using System.Threading;
namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial class SalesOrgHierarchyManager : DomainService
    {


        public async Task<SalesOrgHierarchy> CreateAsync(
        Guid salesOrgHeaderId, Guid? parentId, string code, string name, int level, bool isRoute, bool isSellingZone, string hierarchyCode, bool active)
        {
            Check.NotNull(salesOrgHeaderId, nameof(salesOrgHeaderId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHierarchyConsts.CodeMaxLength, SalesOrgHierarchyConsts.CodeMinLength);
            Check.Range(level, nameof(level), SalesOrgHierarchyConsts.LevelMinLength, SalesOrgHierarchyConsts.LevelMaxLength);
            Check.NotNullOrWhiteSpace(hierarchyCode, nameof(hierarchyCode));

            var salesOrgHierarchy = new SalesOrgHierarchy(
             GuidGenerator.Create(),
             salesOrgHeaderId, parentId, code, name, level, isRoute, isSellingZone, hierarchyCode, active
             );
            await ValidateOrganizationUnitAsync(salesOrgHierarchy);
            if(parentId is null){
                salesOrgHierarchy.Level = 0;    
            }else{
                var parent_node = await _salesOrgHierarchyRepository.GetAsync(parentId.Value);
                salesOrgHierarchy.Level = parent_node.Level + 1;
                if(parent_node.IsRoute == true){
                    throw new UserFriendlyException("Route cannot add subroute.");
                    
                }
            }
            salesOrgHierarchy.HierarchyCode = await GetNextChildCodeAsync(parentId);
            // Check isSelling Zone, isRoute
            
            
            return await _salesOrgHierarchyRepository.InsertAsync(salesOrgHierarchy);
        }

        public virtual async Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);
            
            if (lastChild != null)
            {
                return SalesOrgHierarchy.CalculateNextCode(lastChild.HierarchyCode);
                
            }

            var parentCode = parentId != null
                ? await GetCodeOrDefaultAsync(parentId.Value)
                : null;

            return SalesOrgHierarchy.AppendCode(
                parentCode,
                SalesOrgHierarchy.CreateCode(1)
            );
        }

        public virtual async Task<SalesOrgHierarchy> GetLastChildOrNullAsync(Guid? parentId)
        {
            var children = await _salesOrgHierarchyRepository.GetChildrenAsync(parentId);
            return children.OrderBy(c => c.HierarchyCode).LastOrDefault();
        }

        public virtual async Task<string> GetCodeOrDefaultAsync(Guid id)
        {
            var ou = await _salesOrgHierarchyRepository.GetAsync(id);
            return ou?.HierarchyCode;
        }


        //Validation
        protected virtual async Task ValidateOrganizationUnitAsync(SalesOrgHierarchy organizationUnit)
        {
            var siblings = (await FindChildrenAsync(organizationUnit.ParentId))
                .Where(ou => ou.Id != organizationUnit.Id)
                .ToList();

            if (siblings.Any(ou => ou.Name == organizationUnit.Name))
            {
                throw new BusinessException("102")
                    .WithData("0", organizationUnit.Name);
            }
        }

        // Move 
        [UnitOfWork]
        public virtual async Task MoveAsync(Guid id, Guid? parentId)
        {
            var organizationUnit = await _salesOrgHierarchyRepository.GetAsync(id);
            if (organizationUnit.ParentId == parentId)
            {
                return;
            }

            //Should find children before Code change
            var children = await FindChildrenAsync(id, true);

            //Store old code of OU
            var oldCode = organizationUnit.HierarchyCode;

            //Move OU
            organizationUnit.HierarchyCode = await GetNextChildCodeAsync(parentId);
            organizationUnit.ParentId = parentId;


            await ValidateOrganizationUnitAsync(organizationUnit);

            //Update Children Codes
            foreach (var child in children)
            {
                child.Code = SalesOrgHierarchy.AppendCode(organizationUnit.HierarchyCode, SalesOrgHierarchy.GetRelativeCode(child.HierarchyCode, oldCode));
                await _salesOrgHierarchyRepository.UpdateAsync(child);
            }

            await _salesOrgHierarchyRepository.UpdateAsync(organizationUnit);
        }


        public async Task<List<SalesOrgHierarchy>> FindChildrenAsync(Guid? parentId, bool recursive = false)
        {
            if (!recursive)
            {
                return await _salesOrgHierarchyRepository.GetChildrenAsync(parentId, includeDetails: true);
            }

            if (!parentId.HasValue)
            {
                return await _salesOrgHierarchyRepository.GetListAsync(includeDetails: true);
            }

            var code = await GetCodeOrDefaultAsync(parentId.Value);

            return await _salesOrgHierarchyRepository.GetAllChildrenWithParentCodeAsync(code, parentId, includeDetails: true);
        }

        public async Task<SalesOrgHierarchy> UpdateAsync(
            Guid id,
            Guid salesOrgHeaderId, Guid? parentId, string code, string name, int level, bool isRoute, bool isSellingZone, string hierarchyCode, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(salesOrgHeaderId, nameof(salesOrgHeaderId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHierarchyConsts.CodeMaxLength, SalesOrgHierarchyConsts.CodeMinLength);
            Check.Range(level, nameof(level), SalesOrgHierarchyConsts.LevelMinLength, SalesOrgHierarchyConsts.LevelMaxLength);
            Check.NotNullOrWhiteSpace(hierarchyCode, nameof(hierarchyCode));

            var queryable = await _salesOrgHierarchyRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var salesOrgHierarchy = await AsyncExecuter.FirstOrDefaultAsync(query);

            salesOrgHierarchy.SalesOrgHeaderId = salesOrgHeaderId;
            salesOrgHierarchy.ParentId = parentId;
            salesOrgHierarchy.Code = code;
            salesOrgHierarchy.Name = name;
            salesOrgHierarchy.Level = level;
            salesOrgHierarchy.IsRoute = isRoute;
            salesOrgHierarchy.IsSellingZone = isSellingZone;
            salesOrgHierarchy.HierarchyCode = hierarchyCode;
            salesOrgHierarchy.Active = active;
            await ValidateOrganizationUnitAsync(salesOrgHierarchy);
            salesOrgHierarchy.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesOrgHierarchyRepository.UpdateAsync(salesOrgHierarchy);
        }

        //Delete
        [UnitOfWork]
        public virtual async Task DeleteAsync(Guid id)
        {
            var children = await FindChildrenAsync(id, true);

            foreach (var child in children)
            {
                await _salesOrgHierarchyRepository.RemoveAllMembersAsync(child);
                
                await _salesOrgHierarchyRepository.DeleteAsync(child);
            }

            var organizationUnit = await _salesOrgHierarchyRepository.GetAsync(id);

            await _salesOrgHierarchyRepository.RemoveAllMembersAsync(organizationUnit);
            //await OrganizationUnitRepository.RemoveAllRolesAsync(organizationUnit);
            await _salesOrgHierarchyRepository.DeleteAsync(id);
        }

    }
}