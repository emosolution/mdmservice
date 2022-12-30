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
using DMSpro.OMS.MdmService.SalesChannels;
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
namespace DMSpro.OMS.MdmService.SalesChannels
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.SalesChannels.Default)]
    public class SalesChannelsAppService : ApplicationService, ISalesChannelsAppService
    {
        private readonly IDistributedCache<SalesChannelExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISalesChannelRepository _salesChannelRepository;
        private readonly SalesChannelManager _salesChannelManager;

        public SalesChannelsAppService(ISalesChannelRepository salesChannelRepository, SalesChannelManager salesChannelManager, IDistributedCache<SalesChannelExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _salesChannelRepository = salesChannelRepository;
            _salesChannelManager = salesChannelManager;
        }

        public virtual async Task<PagedResultDto<SalesChannelDto>> GetListAsync(GetSalesChannelsInput input)
        {
            var totalCount = await _salesChannelRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active);
            var items = await _salesChannelRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalesChannelDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesChannel>, List<SalesChannelDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _salesChannelRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<SalesChannel>, IEnumerable<SalesChannelDto>>(results.data.Cast<SalesChannel>());
            
            return results;
                
        }
        public virtual async Task<SalesChannelDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesChannel, SalesChannelDto>(await _salesChannelRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesChannels.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesChannelRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesChannels.Create)]
        public virtual async Task<SalesChannelDto> CreateAsync(SalesChannelCreateDto input)
        {

            var salesChannel = await _salesChannelManager.CreateAsync(
            input.Code, input.Name, input.Description, input.Active
            );

            return ObjectMapper.Map<SalesChannel, SalesChannelDto>(salesChannel);
        }

        [Authorize(MdmServicePermissions.SalesChannels.Edit)]
        public virtual async Task<SalesChannelDto> UpdateAsync(Guid id, SalesChannelUpdateDto input)
        {

            var salesChannel = await _salesChannelManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Description, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesChannel, SalesChannelDto>(salesChannel);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesChannelExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _salesChannelRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Description, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SalesChannel>, List<SalesChannelExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SalesChannels.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalesChannelExcelDownloadTokenCacheItem { Token = token },
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