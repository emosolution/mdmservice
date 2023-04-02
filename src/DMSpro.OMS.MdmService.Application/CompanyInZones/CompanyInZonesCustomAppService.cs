using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;

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

            Check.NotNull(input.SalesOrgHierarchyId, nameof(input.SalesOrgHierarchyId));
            Check.NotNull(input.CompanyId, nameof(input.CompanyId));
            Check.NotNull(input.EffectiveDate, nameof(input.EffectiveDate));

            var companyInZone = new CompanyInZone(
                GuidGenerator.Create(),
                input.SalesOrgHierarchyId, input.CompanyId, input.ItemGroupId, input.EffectiveDate, input.EndDate);

            var record = await _companyInZoneRepository.InsertAsync(companyInZone);

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(record);
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

            Check.NotNull(input.SalesOrgHierarchyId, nameof(input.SalesOrgHierarchyId));
            Check.NotNull(input.CompanyId, nameof(input.CompanyId));
            Check.NotNull(input.EffectiveDate, nameof(input.EffectiveDate));

            var companyInZone = await _companyInZoneRepository.GetAsync(id);

            companyInZone.SalesOrgHierarchyId = input.SalesOrgHierarchyId;
            companyInZone.CompanyId = input.CompanyId;
            companyInZone.ItemGroupId = input.ItemGroupId;
            companyInZone.EffectiveDate = input.EffectiveDate;
            companyInZone.EndDate = input.EndDate;

            companyInZone.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            var record = await _companyInZoneRepository.UpdateAsync(companyInZone);

            return ObjectMapper.Map<CompanyInZone, CompanyInZoneDto>(record);
        }
    }
}