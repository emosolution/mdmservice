using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Customers;
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

namespace DMSpro.OMS.MdmService.MCPDetails
{

    [Authorize(MdmServicePermissions.MCPs.Default)]
    public partial class MCPDetailsAppService
    {
        public virtual async Task<PagedResultDto<MCPDetailWithNavigationPropertiesDto>> GetListAsync(GetMCPDetailsInput input)
        {
            var totalCount = await _mCPDetailRepository.GetCountAsync(input.FilterText, input.Code, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4, input.CustomerId, input.MCPHeaderId);
            var items = await _mCPDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4, input.CustomerId, input.MCPHeaderId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MCPDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MCPDetailWithNavigationProperties>, List<MCPDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<MCPDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<MCPDetailWithNavigationProperties, MCPDetailWithNavigationPropertiesDto>
                (await _mCPDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<MCPDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MCPDetail, MCPDetailDto>(await _mCPDetailRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetMCPHeaderLookupAsync(LookupRequestDto input)
        {
            var query = (await _mCPHeaderRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<MCPHeader>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MCPHeader>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.MCPs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mCPDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.MCPs.Create)]
        public virtual async Task<MCPDetailDto> CreateAsync(MCPDetailCreateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.MCPHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPHeader"]]);
            }
            await CheckCodeUniqueness(input.Code);
            var mcpDetail = await _mCPDetailManager.CreateAsync(
            input.CustomerId, input.MCPHeaderId, input.Code, input.EffectiveDate, input.Distance, input.VisitOrder, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4, input.EndDate
            );

            return ObjectMapper.Map<MCPDetail, MCPDetailDto>(mcpDetail);
        }

        [Authorize(MdmServicePermissions.MCPs.Edit)]
        public virtual async Task<MCPDetailDto> UpdateAsync(Guid id, MCPDetailUpdateDto input)
        {
            if (input.CustomerId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Customer"]]);
            }
            if (input.MCPHeaderId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPHeader"]]);
            }
            await CheckCodeUniqueness(input.Code, id);
            var mcpDetail = await _mCPDetailManager.UpdateAsync(
            id,
            input.CustomerId, input.MCPHeaderId, input.Code, input.EffectiveDate, input.Distance, input.VisitOrder, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MCPDetail, MCPDetailDto>(mcpDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _mCPDetailRepository.GetListAsync(input.FilterText, input.Code, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MCPDetail>, List<MCPDetailExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MCPDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MCPDetailExcelDownloadTokenCacheItem { Token = token },
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