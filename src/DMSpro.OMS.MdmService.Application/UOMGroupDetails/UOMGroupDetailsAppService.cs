using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
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
using DMSpro.OMS.MdmService.UOMGroupDetails;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.UOMGroupDetails
{

    [Authorize(MdmServicePermissions.UOMGroupDetails.Default)]
    public class UOMGroupDetailsAppService : ApplicationService, IUOMGroupDetailsAppService
    {
        private readonly IDistributedCache<UOMGroupDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly UOMGroupDetailManager _uOMGroupDetailManager;
        private readonly IRepository<UOMGroup, Guid> _uOMGroupRepository;
        private readonly IRepository<UOM, Guid> _uOMRepository;

        public UOMGroupDetailsAppService(IUOMGroupDetailRepository uOMGroupDetailRepository, UOMGroupDetailManager uOMGroupDetailManager, IDistributedCache<UOMGroupDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<UOMGroup, Guid> uOMGroupRepository, IRepository<UOM, Guid> uOMRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _uOMGroupDetailManager = uOMGroupDetailManager; _uOMGroupRepository = uOMGroupRepository;
            _uOMRepository = uOMRepository;
        }

        public virtual async Task<PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>> GetListAsync(GetUOMGroupDetailsInput input)
        {
            var totalCount = await _uOMGroupDetailRepository.GetCountAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active, input.UOMGroupId, input.AltUOMId, input.BaseUOMId);
            var items = await _uOMGroupDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active, input.UOMGroupId, input.AltUOMId, input.BaseUOMId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroupDetailWithNavigationProperties>, List<UOMGroupDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<UOMGroupDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<UOMGroupDetailWithNavigationProperties, UOMGroupDetailWithNavigationPropertiesDto>
                (await _uOMGroupDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _uOMGroupDetailRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<UOMGroupDetail>, IEnumerable<UOMGroupDetailDto>>(results.data.Cast<UOMGroupDetail>());
            
            return results;
                
        }

        public virtual async Task<UOMGroupDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(await _uOMGroupDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOMGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOM>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOM>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _uOMGroupDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Create)]
        public virtual async Task<UOMGroupDetailDto> CreateAsync(UOMGroupDetailCreateDto input)
        {
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.AltUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.BaseUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var uOMGroupDetail = await _uOMGroupDetailManager.CreateAsync(
            input.UOMGroupId, input.AltUOMId, input.BaseUOMId, input.AltQty, input.BaseQty, input.Active
            );

            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(uOMGroupDetail);
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Edit)]
        public virtual async Task<UOMGroupDetailDto> UpdateAsync(Guid id, UOMGroupDetailUpdateDto input)
        {
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.AltUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.BaseUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var uOMGroupDetail = await _uOMGroupDetailManager.UpdateAsync(
            id,
            input.UOMGroupId, input.AltUOMId, input.BaseUOMId, input.AltQty, input.BaseQty, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(uOMGroupDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _uOMGroupDetailRepository.GetListAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UOMGroupDetail>, List<UOMGroupDetailExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "UOMGroupDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UOMGroupDetailExcelDownloadTokenCacheItem { Token = token },
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