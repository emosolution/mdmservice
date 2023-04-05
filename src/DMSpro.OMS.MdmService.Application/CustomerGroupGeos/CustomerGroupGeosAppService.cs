using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Caching;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{

    [Authorize(MdmServicePermissions.CustomerGroupGeos.Default)]
    public class CustomerGroupGeosAppService : ApplicationService, ICustomerGroupGeosAppService
    {
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;
        private readonly CustomerGroupGeoManager _customerGroupGeoManager;

        public CustomerGroupGeosAppService(ICustomerGroupGeoRepository customerGroupGeoRepository, CustomerGroupGeoManager customerGroupGeoManager, IDistributedCache<CustomerGroupGeoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<CustomerGroup, Guid> customerGroupRepository, IRepository<GeoMaster, Guid> geoMasterRepository)
        {
            _customerGroupGeoRepository = customerGroupGeoRepository;
            _customerGroupGeoManager = customerGroupGeoManager;
        }

        public virtual async Task<CustomerGroupGeoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(await _customerGroupGeoRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.CustomerGroupGeos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupGeoRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroupGeos.Create)]
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
            if (input.GeoMaster1Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster2Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster3Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster4Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            var customerGroupGeo = await _customerGroupGeoManager.CreateAsync(
            input.CustomerGroupId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Description, input.Active
            );

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }

        [Authorize(MdmServicePermissions.CustomerGroupGeos.Edit)]
        public virtual async Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.GeoMaster0Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster1Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster2Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster3Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }
            if (input.GeoMaster4Id == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["GeoMaster"]]);
            }

            var customerGroupGeo = await _customerGroupGeoManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }
    }
}