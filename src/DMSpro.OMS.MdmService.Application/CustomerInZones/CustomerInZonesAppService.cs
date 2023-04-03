using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.CustomerInZones
{

    [Authorize(MdmServicePermissions.CustomerInZones.Default)]
    public partial class CustomerInZonesAppService
    { 
        public virtual async Task<CustomerInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(await _customerInZoneRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerInZones.Create)]
        public virtual async Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);

            var customerInZone = await _customerInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.CustomerId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(customerInZone);
        }

        [Authorize(MdmServicePermissions.CustomerInZones.Edit)]
        public virtual async Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);

            var customerInZone = await _customerInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.CustomerId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerInZone, CustomerInZoneDto>(customerInZone);
        }
    }
}