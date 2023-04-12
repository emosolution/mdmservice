using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupGeosAppService
    {   
        public virtual async Task<CustomerGroupGeoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(await _customerGroupGeoRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var customerGroupGeo = await _customerGroupGeoRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupGeo.CustomerGroupId);
            await _customerGroupGeoRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Create)]
        public virtual async Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.GeoMaster0Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            await CheckCustomerGroup(input.CustomerGroupId);
            await _geoMastersInternalAppService.CheckAllGeoInputs(input.GeoMaster0Id, input.GeoMaster1Id,
                input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id);

            var customerGroupGeo = await _customerGroupGeoManager.CreateAsync(
                input.CustomerGroupId, 
                input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, 
                null, true);

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
        public virtual async Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input)
        {
            if (input.GeoMaster0Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            var customerGroupGeo = await _customerGroupGeoRepository.GetAsync(id);
            await CheckCustomerGroup(customerGroupGeo.CustomerGroupId);
            await _geoMastersInternalAppService.CheckAllGeoInputs(input.GeoMaster0Id, input.GeoMaster1Id,
                input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id);
            await _customerGroupGeoManager.UpdateAsync(
                id,
                input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, 
                null, true,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }

        private async Task CheckCustomerGroup(Guid customerId)
        {
            var customerGroup = await _customerGroupRepository.GetAsync(customerId);
            if (customerGroup.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupGeosAppService:550"], code: "0");
            }
            if (customerGroup.GroupBy != CustomerGroups.Type.GEO)
            {
                throw new UserFriendlyException(message: L["Error:CustomerGroupGeosAppService:551"], code: "1");
            }
        }
    }
}