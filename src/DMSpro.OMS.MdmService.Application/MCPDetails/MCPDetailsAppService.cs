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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.MCPDetails;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.MCPDetails
{

    [Authorize(MdmServicePermissions.MCPDetails.Default)]
    public class MCPDetailsAppService : ApplicationService, IMCPDetailsAppService
    {
        private readonly IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly MCPDetailManager _mCPDetailManager;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<MCPHeader, Guid> _mCPHeaderRepository;

        public MCPDetailsAppService(IMCPDetailRepository mCPDetailRepository, MCPDetailManager mCPDetailManager, IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, 
            IRepository<Customer, Guid> customerRepository, 
            IRepository<MCPHeader, Guid> mCPHeaderRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mCPDetailRepository = mCPDetailRepository;
            _mCPDetailManager = mCPDetailManager; 
            _customerRepository = customerRepository;
            _mCPHeaderRepository = mCPHeaderRepository;
        }

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


        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _mCPDetailRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<MCPDetail>, IEnumerable<MCPDetailDto>>(results.data.Cast<MCPDetail>());
            
            return results;
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

        [Authorize(MdmServicePermissions.MCPDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mCPDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.MCPDetails.Create)]
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

            var mcpDetail = await _mCPDetailManager.CreateAsync(
            input.CustomerId, input.MCPHeaderId, input.Code, input.EffectiveDate, input.Distance, input.VisitOrder, input.Monday, input.Tuesday, input.Wednesday, input.Thursday, input.Friday, input.Saturday, input.Sunday, input.Week1, input.Week2, input.Week3, input.Week4, input.EndDate
            );

            return ObjectMapper.Map<MCPDetail, MCPDetailDto>(mcpDetail);
        }

        [Authorize(MdmServicePermissions.MCPDetails.Edit)]
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