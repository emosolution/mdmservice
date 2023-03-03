using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.CustomerImages
{

    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomerImagesAppService
    { 

        public virtual async Task<PagedResultDto<CustomerImageWithNavigationPropertiesDto>> GetListAsync(GetCustomerImagesInput input)
        {
            var totalCount = await _customerImageRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.IsPOSM, input.FileId, input.CustomerId, input.POSMItemId);
            var items = await _customerImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.IsPOSM, input.FileId, input.CustomerId, input.POSMItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerImageWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerImageWithNavigationProperties>, List<CustomerImageWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerImageWithNavigationProperties, CustomerImageWithNavigationPropertiesDto>
                (await _customerImageRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerImageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerImage, CustomerImageDto>(await _customerImageRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Customer>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Item>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Item>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerImageExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerImages = await _customerImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.IsPOSM, input.FileId);
            var items = customerImages.Select(item => new
            {
                Description = item.CustomerImage.Description,
                Active = item.CustomerImage.Active,
                IsAvatar = item.CustomerImage.IsAvatar,
                IsPOSM = item.CustomerImage.IsPOSM,
                FileId = item.CustomerImage.FileId,

                CustomerCode = item.Customer?.Code,
                ItemCode = item.Item?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerImages.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerImageExcelDownloadTokenCacheItem { Token = token },
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