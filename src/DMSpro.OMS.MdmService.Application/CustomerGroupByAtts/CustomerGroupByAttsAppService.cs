using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{

    [Authorize(MdmServicePermissions.CustomerGroupByAtts.Default)]
    public partial class CustomerGroupByAttsAppService 
    {
        public virtual async Task<PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByAttsInput input)
        {
            var totalCount = await _customerGroupByAttRepository.GetCountAsync(input.FilterText, input.ValueCode, input.ValueName, input.CustomerGroupId, input.CusAttributeValueId);
            var items = await _customerGroupByAttRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ValueCode, input.ValueName, input.CustomerGroupId, input.CusAttributeValueId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerGroupByAttWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroupByAttWithNavigationProperties>, List<CustomerGroupByAttWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerGroupByAttWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByAttWithNavigationProperties, CustomerGroupByAttWithNavigationPropertiesDto>
                (await _customerGroupByAttRepository.GetWithNavigationPropertiesAsync(id));
                
        }

        public virtual async Task<CustomerGroupByAttDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(await _customerGroupByAttRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _cusAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CusAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CusAttributeValue>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerGroupByAttRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Create)]
        public virtual async Task<CustomerGroupByAttDto> CreateAsync(CustomerGroupByAttCreateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CusAttributeValueId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CusAttributeValue"]]);
            }

            var customerGroupByAtt = await _customerGroupByAttManager.CreateAsync(
            input.CustomerGroupId, input.CusAttributeValueId, input.ValueCode, input.ValueName
            );

            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(customerGroupByAtt);
        }

        [Authorize(MdmServicePermissions.CustomerGroupByAtts.Edit)]
        public virtual async Task<CustomerGroupByAttDto> UpdateAsync(Guid id, CustomerGroupByAttUpdateDto input)
        {
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }
            if (input.CusAttributeValueId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CusAttributeValue"]]);
            }

            var customerGroupByAtt = await _customerGroupByAttManager.UpdateAsync(
            id,
            input.CustomerGroupId, input.CusAttributeValueId, input.ValueCode, input.ValueName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerGroupByAtt, CustomerGroupByAttDto>(customerGroupByAtt);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByAttExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerGroupByAtts = await _customerGroupByAttRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.ValueCode, input.ValueName);
            var items = customerGroupByAtts.Select(item => new
            {
                ValueCode = item.CustomerGroupByAtt.ValueCode,
                ValueName = item.CustomerGroupByAtt.ValueName,

                CustomerGroupCode = item.CustomerGroup?.Code,
                CusAttributeValueAttrValName = item.CusAttributeValue?.AttrValName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerGroupByAtts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerGroupByAttExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}