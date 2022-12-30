using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyManager : DomainService
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyManager(ISalesOrgHierarchyRepository salesOrgHierarchyRepository)
        {
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
        }

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

            return await _salesOrgHierarchyRepository.InsertAsync(salesOrgHierarchy);
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

            salesOrgHierarchy.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesOrgHierarchyRepository.UpdateAsync(salesOrgHierarchy);
        }

    }
}