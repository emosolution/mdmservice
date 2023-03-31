using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial class CompanyInZonesAppService
    {
        public virtual async Task<CompanyInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(await _companyInZoneRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Create)]
        public virtual async Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyInZone = await _companyInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.CompanyId, input.ItemGroupId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(companyInZone);
        }

        [Authorize(MdmServicePermissions.CompanyInZones.Edit)]
        public virtual async Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyInZone = await _companyInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.CompanyId, input.ItemGroupId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(companyInZone);
        }
    }
}