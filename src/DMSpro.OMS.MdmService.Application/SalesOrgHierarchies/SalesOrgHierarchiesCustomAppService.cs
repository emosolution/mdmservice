using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{

    [Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
    public partial class SalesOrgHierarchiesAppService
    {
        public virtual async Task<SalesOrgHierarchyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(await _salesOrgHierarchyRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgHierarchyRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateRootAsync(SalesOrgHierarchyCreateRootDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }
            var company = await _companyIdentityUserAssignmentsInternalAppService.GetCurrentlySelectedCompanyAsync();
            await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync("", company.Id);
            
            var salesOrgHierarchy = await _salesOrgHierarchyManager.CreateAsync(
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateSubAsync(SalesOrgHierarchyCreateSubDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }
            //await CheckCodeUniqueness(input.Code);
            var salesOrgHierarchy = await _salesOrgHierarchyManager.CreateAsync(
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateRouteAsync(SalesOrgHierarchyCreateRouteDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }
            //await CheckCodeUniqueness(input.Code);
            var salesOrgHierarchy = await _salesOrgHierarchyManager.CreateAsync(
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Edit)]
        public virtual async Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHeader"]]);
            }
            //await CheckCodeUniqueness(input.Code, id);
            var salesOrgHierarchy = await _salesOrgHierarchyManager.UpdateAsync(
            id,
            input.SalesOrgHeaderId, input.ParentId, input.Code, input.Name, input.Level, input.IsRoute, input.IsSellingZone, input.HierarchyCode, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(salesOrgHierarchy);
        }
    }
}