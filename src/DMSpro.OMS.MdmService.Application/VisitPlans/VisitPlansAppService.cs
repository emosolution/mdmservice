using DMSpro.OMS.MdmService.MCPDetails;
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

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.VisitPlans
{

    [Authorize(MdmServicePermissions.VisitPlans.Default)]
    public class VisitPlansAppService : ApplicationService, IVisitPlansAppService
    {
        private readonly IDistributedCache<VisitPlanExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly VisitPlanManager _visitPlanManager;
        private readonly IRepository<MCPDetail, Guid> _mCPDetailRepository;

        public VisitPlansAppService(IVisitPlanRepository visitPlanRepository, VisitPlanManager visitPlanManager, IDistributedCache<VisitPlanExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<MCPDetail, Guid> mCPDetailRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _visitPlanRepository = visitPlanRepository;
            _visitPlanManager = visitPlanManager; _mCPDetailRepository = mCPDetailRepository;
        }

        public virtual async Task<PagedResultDto<VisitPlanWithNavigationPropertiesDto>> GetListAsync(GetVisitPlansInput input)
        {
            var totalCount = await _visitPlanRepository.GetCountAsync(input.FilterText, input.DateVisitMin, input.DateVisitMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.DayOfWeek, input.WeekMin, input.WeekMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.MCPDetailId);
            var items = await _visitPlanRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.DateVisitMin, input.DateVisitMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.DayOfWeek, input.WeekMin, input.WeekMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax, input.MCPDetailId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VisitPlanWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VisitPlanWithNavigationProperties>, List<VisitPlanWithNavigationPropertiesDto>>(items)
            };
        }
        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _visitPlanRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<VisitPlan>, IEnumerable<VisitPlanDto>>(results.data.Cast<VisitPlan>());
            
            return results;
                
        }

        public virtual async Task<VisitPlanWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VisitPlanWithNavigationProperties, VisitPlanWithNavigationPropertiesDto>
                (await _visitPlanRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VisitPlanDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(await _visitPlanRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetMCPDetailLookupAsync(LookupRequestDto input)
        {
            var query = (await _mCPDetailRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<MCPDetail>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MCPDetail>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.VisitPlans.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _visitPlanRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Create)]
        public virtual async Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
        {
            if (input.MCPDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPDetail"]]);
            }

            var visitPlan = await _visitPlanManager.CreateAsync(
            input.MCPDetailId, input.DateVisit, input.Distance, input.VisitOrder, input.DayOfWeek, input.Week, input.Month, input.Year
            );

            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(visitPlan);
        }

        [Authorize(MdmServicePermissions.VisitPlans.Edit)]
        public virtual async Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
        {
            if (input.MCPDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["MCPDetail"]]);
            }

            var visitPlan = await _visitPlanManager.UpdateAsync(
            id,
            input.MCPDetailId, input.DateVisit, input.Distance, input.VisitOrder, input.DayOfWeek, input.Week, input.Month, input.Year, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VisitPlan, VisitPlanDto>(visitPlan);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitPlanExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _visitPlanRepository.GetListAsync(input.FilterText, input.DateVisitMin, input.DateVisitMax, input.DistanceMin, input.DistanceMax, input.VisitOrderMin, input.VisitOrderMax, input.DayOfWeek, input.WeekMin, input.WeekMax, input.MonthMin, input.MonthMax, input.YearMin, input.YearMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<VisitPlan>, List<VisitPlanExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VisitPlans.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VisitPlanExcelDownloadTokenCacheItem { Token = token },
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