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
namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{

    [Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
    public class SalesOrgHeadersAppService : ApplicationService, ISalesOrgHeadersAppService
    {
        private readonly IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
        private readonly SalesOrgHeaderManager _salesOrgHeaderManager;

        public SalesOrgHeadersAppService(ISalesOrgHeaderRepository salesOrgHeaderRepository, SalesOrgHeaderManager salesOrgHeaderManager, IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _salesOrgHeaderRepository = salesOrgHeaderRepository;
            _salesOrgHeaderManager = salesOrgHeaderManager;
        }

        public virtual async Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input)
        {
            var totalCount = await _salesOrgHeaderRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Active);
            var items = await _salesOrgHeaderRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalesOrgHeaderDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHeader>, List<SalesOrgHeaderDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _salesOrgHeaderRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<SalesOrgHeader>, IEnumerable<SalesOrgHeaderDto>>(results.data.Cast<SalesOrgHeader>());
            
            return results;
                
        }
        public virtual async Task<SalesOrgHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(await _salesOrgHeaderRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgHeaderRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Create)]
        public virtual async Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
        {

            var salesOrgHeader = await _salesOrgHeaderManager.CreateAsync(
            input.Code, input.Name, input.Active
            );

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }

        [Authorize(MdmServicePermissions.SalesOrgHeaders.Edit)]
        public virtual async Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input)
        {

            var salesOrgHeader = await _salesOrgHeaderManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgHeader, SalesOrgHeaderDto>(salesOrgHeader);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _salesOrgHeaderRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SalesOrgHeader>, List<SalesOrgHeaderExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SalesOrgHeaders.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalesOrgHeaderExcelDownloadTokenCacheItem { Token = token },
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