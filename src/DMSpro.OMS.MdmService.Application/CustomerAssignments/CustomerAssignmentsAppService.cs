using DMSpro.OMS.MdmService.Companies;
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
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{

    [Authorize(MdmServicePermissions.CustomerAssignments.Default)]
    public partial class CustomerAssignmentsAppService
    {
        public virtual async Task<PagedResultDto<CustomerAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAssignmentsInput input)
        {
            var totalCount = await _customerAssignmentRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.CompanyId, input.CustomerId);
            var items = await _customerAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.CompanyId, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAssignmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAssignmentWithNavigationProperties>, List<CustomerAssignmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAssignmentWithNavigationProperties, CustomerAssignmentWithNavigationPropertiesDto>
                (await _customerAssignmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAssignment, CustomerAssignmentDto>(await _customerAssignmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(MdmServicePermissions.CustomerAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CustomerAssignments.Create)]
        public virtual async Task<CustomerAssignmentDto> CreateAsync(CustomerAssignmentCreateDto input)
        {
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);

            var customerAssignment = await _customerAssignmentManager.CreateAsync(
            input.CompanyId, input.CustomerId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<CustomerAssignment, CustomerAssignmentDto>(customerAssignment);
        }

        [Authorize(MdmServicePermissions.CustomerAssignments.Edit)]
        public virtual async Task<CustomerAssignmentDto> UpdateAsync(Guid id, CustomerAssignmentUpdateDto input)
        {
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);

            var customerAssignment = await _customerAssignmentManager.UpdateAsync(
            id,
            input.CompanyId, input.CustomerId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAssignment, CustomerAssignmentDto>(customerAssignment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAssignmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerAssignmentRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CustomerAssignment>, List<CustomerAssignmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAssignments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAssignmentExcelDownloadTokenCacheItem { Token = token },
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
