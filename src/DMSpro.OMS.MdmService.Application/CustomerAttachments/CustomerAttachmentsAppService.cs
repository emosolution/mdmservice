using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{

    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomerAttachmentsAppService : ApplicationService, ICustomerAttachmentsAppService
    {
        private readonly IDistributedCache<CustomerAttachmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;
        private readonly CustomerAttachmentManager _customerAttachmentManager;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerAttachmentsAppService(ICustomerAttachmentRepository customerAttachmentRepository, CustomerAttachmentManager customerAttachmentManager,
            IRepository<Customer, Guid> customerRepository,
            IDistributedCache<CustomerAttachmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerAttachmentRepository = customerAttachmentRepository;
            _customerAttachmentManager = customerAttachmentManager; 
            _customerRepository = customerRepository;
        }

        public virtual async Task<PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttachmentsInput input)
        {
            var totalCount = await _customerAttachmentRepository.GetCountAsync(input.FilterText, input.url, input.Description, input.Active, input.CustomerId);
            var items = await _customerAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.url, input.Description, input.Active, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttachmentWithNavigationProperties>, List<CustomerAttachmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttachmentWithNavigationProperties, CustomerAttachmentWithNavigationPropertiesDto>
                (await _customerAttachmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerAttachmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAttachment, CustomerAttachmentDto>(await _customerAttachmentRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.Customers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAttachmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerAttachmentDto> CreateAsync(CustomerAttachmentCreateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerAttachment = await _customerAttachmentManager.CreateAsync(
            input.CustomerId, input.url, input.Description, input.Active
            );

            return ObjectMapper.Map<CustomerAttachment, CustomerAttachmentDto>(customerAttachment);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerAttachmentDto> UpdateAsync(Guid id, CustomerAttachmentUpdateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }

            var customerAttachment = await _customerAttachmentManager.UpdateAsync(
            id,
            input.CustomerId, input.url, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAttachment, CustomerAttachmentDto>(customerAttachment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttachmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerAttachmentRepository.GetListAsync(input.FilterText, input.url, input.Description, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerAttachment>, List<CustomerAttachmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAttachments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAttachmentExcelDownloadTokenCacheItem { Token = token },
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