using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.PriceLists;
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

namespace DMSpro.OMS.MdmService.PricelistAssignments
{

    [Authorize(MdmServicePermissions.PriceListAssignments.Default)]
    public partial class PricelistAssignmentsAppService
    {
        public virtual async Task<PagedResultDto<PricelistAssignmentWithNavigationPropertiesDto>> GetListAsync(GetPricelistAssignmentsInput input)
        {
            var totalCount = await _pricelistAssignmentRepository.GetCountAsync(input.FilterText, input.Description, input.PriceListId, input.CustomerGroupId);
            var items = await _pricelistAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.PriceListId, input.CustomerGroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PricelistAssignmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PricelistAssignmentWithNavigationProperties>, List<PricelistAssignmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PricelistAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PricelistAssignmentWithNavigationProperties, PricelistAssignmentWithNavigationPropertiesDto>
                (await _pricelistAssignmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PricelistAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(await _pricelistAssignmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceList>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(MdmServicePermissions.PriceListAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _pricelistAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceListAssignments.Create)]
        public virtual async Task<PricelistAssignmentDto> CreateAsync(PricelistAssignmentCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            var pricelistAssignment = await _pricelistAssignmentManager.CreateAsync(
            input.PriceListId, input.CustomerGroupId, input.Description
            );

            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(pricelistAssignment);
        }

        [Authorize(MdmServicePermissions.PriceListAssignments.Edit)]
        public virtual async Task<PricelistAssignmentDto> UpdateAsync(Guid id, PricelistAssignmentUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CustomerGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerGroup"]]);
            }

            var pricelistAssignment = await _pricelistAssignmentManager.UpdateAsync(
            id,
            input.PriceListId, input.CustomerGroupId, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(pricelistAssignment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PricelistAssignmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var pricelistAssignments = await _pricelistAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description);
            var items = pricelistAssignments.Select(item => new
            {
                Description = item.PricelistAssignment.Description,

                PriceListCode = item.PriceList?.Code,
                CustomerGroupCode = item.CustomerGroup?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PricelistAssignments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PricelistAssignmentExcelDownloadTokenCacheItem { Token = token },
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