using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.GeoMasters;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{

    [Authorize(MdmServicePermissions.CustomerGroups.Default)]
    public partial class CustomerGroupGeosAppService
    {   
        public virtual async Task<CustomerGroupGeoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(await _customerGroupGeoRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _geoMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<GeoMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<GeoMaster>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
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

            var customerGroupGeo = await _customerGroupGeoManager.CreateAsync(
            input.CustomerGroupId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Description, input.Active
            );

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }

        [Authorize(MdmServicePermissions.CustomerGroups.Edit)]
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

            var customerGroupGeo = await _customerGroupGeoManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupGeo, CustomerGroupGeoDto>(customerGroupGeo);
        }
    }
}