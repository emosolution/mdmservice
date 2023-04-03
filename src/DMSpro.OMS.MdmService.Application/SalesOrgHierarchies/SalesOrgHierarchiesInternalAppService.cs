using DMSpro.OMS.MdmService.Localization;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchiesInternalAppService : ApplicationService, 
        ISalesOrgHierarchiesInternalAppService
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchiesInternalAppService(ISalesOrgHierarchyRepository salesOrgHierarchyRepository)
        {
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            LocalizationResource = typeof(MdmServiceResource);
        }

        public virtual async Task<SalesOrgHierarchyDto> CreateAsync(Guid salesOrgHeaderId, 
            Guid? parentId, string code, string name, bool isRoute, bool isSellingZone)
        {
            Check.NotNull(salesOrgHeaderId, nameof(salesOrgHeaderId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHierarchyConsts.CodeMaxLength, 
                SalesOrgHierarchyConsts.CodeMinLength);

            await ValidateOrganizationUnitAsync(null, name, parentId, salesOrgHeaderId);
            
            int level = 0;
            if (parentId != null)
            {
                var parent = await _salesOrgHierarchyRepository.GetAsync(parentId.Value);
                level = parent.Level + 1;
            }
            string hierarchyCode = await GetNextChildCodeAsync(parentId);

            var salesOrgHierarchy = new SalesOrgHierarchy(
                GuidGenerator.Create(),
                salesOrgHeaderId, parentId, code, name, level, isRoute, isSellingZone, hierarchyCode, true, 0);

            var record = await _salesOrgHierarchyRepository.InsertAsync(salesOrgHierarchy);
            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(record);
        }

        public virtual async Task ValidateOrganizationUnitAsync(Guid? id, string name, 
            Guid? parentId, Guid saleOrgId)
        {
            var siblings = (await FindChildrenAsync(parentId))
                .Where(ou => ou.Id != id).Where(ou => ou.SalesOrgHeaderId == saleOrgId)
                .ToList();

            if (siblings.Any(ou => ou.Name == name))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:550"], code: "0").WithData("0", name);
            }
        }

        private async Task<List<SalesOrgHierarchy>> FindChildrenAsync(Guid? parentId, bool recursive = false)
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

            return await _salesOrgHierarchyRepository.GetAllChildrenWithParentCodeAsync(code, 
                parentId, includeDetails: true);
        }

        private async Task<string> GetCodeOrDefaultAsync(Guid id)
        {
            var ou = await _salesOrgHierarchyRepository.GetAsync(id);
            return ou?.HierarchyCode;
        }

        private async Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);

            if (lastChild != null)
            {
                return CalculateNextCode(lastChild.HierarchyCode);

            }

            var parentCode = parentId != null
                ? await GetCodeOrDefaultAsync(parentId.Value)
                : null;

            return AppendCode(parentCode, CreateCode(1));
        }

        private async Task<SalesOrgHierarchy> GetLastChildOrNullAsync(Guid? parentId)
        {
            var children = await _salesOrgHierarchyRepository.GetChildrenAsync(parentId);
            return children.OrderBy(c => c.HierarchyCode).LastOrDefault();
        }

        private static string CreateCode(params int[] numbers)
        {
            if (numbers.IsNullOrEmpty())
            {
                return null;
            }

            return numbers.Select(number => number.ToString(new string('0', SalesOrgHierarchyConsts.CodeUnitLength))).JoinAsString(".");
        }

        /// <summary>
        /// Appends a child code to a parent code.
        /// Example: if parentCode = "00001", childCode = "00042" then returns "00001.00042".
        /// </summary>
        /// <param name="parentCode">Parent code. Can be null or empty if parent is a root.</param>
        /// <param name="childCode">Child code.</param>
        private string AppendCode(string parentCode, string childCode)
        {
            if (childCode.IsNullOrEmpty())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:551"], code: "0");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return childCode;
            }

            return parentCode + "." + childCode;
        }

        /// <summary>
        /// Gets relative code to the parent.
        /// Example: if code = "00019.00055.00001" and parentCode = "00019" then returns "00055.00001".
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="parentCode">The parent code.</param>
        private string GetRelativeCode(string code, string parentCode)
        {
            if (code.IsNullOrEmpty())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:552"], code: "0");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return code;
            }

            if (code.Length == parentCode.Length)
            {
                return null;
            }

            return code.Substring(parentCode.Length + 1);
        }

        /// <summary>
        /// Calculates next code for given code.
        /// Example: if code = "00019.00055.00001" returns "00019.00055.00002".
        /// </summary>
        /// <param name="code">The code.</param>
        private string CalculateNextCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:552"], code: "0");
            }

            var parentCode = GetParentCode(code);
            var lastUnitCode = GetLastUnitCode(code);

            return AppendCode(parentCode, CreateCode(Convert.ToInt32(lastUnitCode) + 1));
        }

        /// <summary>
        /// Gets the last unit code.
        /// Example: if code = "00019.00055.00001" returns "00001".
        /// </summary>
        /// <param name="code">The code.</param>
        private string GetLastUnitCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:552"], code: "0");
            }

            var splittedCode = code.Split('.');
            return splittedCode[^1];
        }

        /// <summary>
        /// Gets parent code.
        /// Example: if code = "00019.00055.00001" returns "00019.00055".
        /// </summary>
        /// <param name="code">The code.</param>
        private string GetParentCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesInternalAppService:552"], code: "0");
            }

            var splittedCode = code.Split('.');
            if (splittedCode.Length == 1)
            {
                return null;
            }

            return splittedCode.Take(splittedCode.Length - 1).JoinAsString(".");
        }

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


            await ValidateOrganizationUnitAsync(id, organizationUnit.Name, 
                organizationUnit.ParentId, organizationUnit.SalesOrgHeaderId);

            //Update Children Codes
            foreach (var child in children)
            {
                child.Code = AppendCode(organizationUnit.HierarchyCode, GetRelativeCode(child.HierarchyCode, oldCode));
                await _salesOrgHierarchyRepository.UpdateAsync(child);
            }

            await _salesOrgHierarchyRepository.UpdateAsync(organizationUnit);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var children = await FindChildrenAsync(id, true);

            foreach (var child in children)
            {
                await _salesOrgHierarchyRepository.RemoveAllMembersAsync(child);

                await _salesOrgHierarchyRepository.DeleteAsync(child);
            }

            var organizationUnit = await _salesOrgHierarchyRepository.GetAsync(id);
            //if (organizationUnit.IsRoute == true)
            //{
            //    //Find other children
            //    var other_children = await FindChildrenAsync(organizationUnit.ParentId, false);
            //    foreach (var child in other_children)
            //    {
            //        if (child.IsRoute)
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            var parentNode = await _salesOrgHierarchyRepository.GetAsync(organizationUnit.ParentId.Value);
            //            parentNode.IsSellingZone = false;
            //            await _salesOrgHierarchyRepository.UpdateAsync(parentNode);
            //        }
            //    }
            //}
            await _salesOrgHierarchyRepository.RemoveAllMembersAsync(organizationUnit);
            //await OrganizationUnitRepository.RemoveAllRolesAsync(organizationUnit);
            await _salesOrgHierarchyRepository.DeleteAsync(id);
        }
    }
}
