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
using DMSpro.OMS.MdmService.VATs;
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
namespace DMSpro.OMS.MdmService.VATs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.VATs.Default)]
    public class VATsAppService : ApplicationService, IVATsAppService
    {
        private readonly IDistributedCache<VATExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVATRepository _vATRepository;
        private readonly VATManager _vATManager;

        public VATsAppService(IVATRepository vATRepository, VATManager vATManager, IDistributedCache<VATExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vATRepository = vATRepository;
            _vATManager = vATManager;
        }

        public virtual async Task<PagedResultDto<VATDto>> GetListAsync(GetVATsInput input)
        {
            var totalCount = await _vATRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.RateMin, input.RateMax);
            var items = await _vATRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.RateMin, input.RateMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VATDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VAT>, List<VATDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _vATRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<VAT>, IEnumerable<VATDto>>(results.data.Cast<VAT>());
            
            return results;
                
        }
        public virtual async Task<VATDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<VAT, VATDto>(await _vATRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.VATs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vATRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.VATs.Create)]
        public virtual async Task<VATDto> CreateAsync(VATCreateDto input)
        {

            var vAT = await _vATManager.CreateAsync(
            input.Code, input.Name, input.Rate
            );

            return ObjectMapper.Map<VAT, VATDto>(vAT);
        }

        [Authorize(MdmServicePermissions.VATs.Edit)]
        public virtual async Task<VATDto> UpdateAsync(Guid id, VATUpdateDto input)
        {

            var vAT = await _vATManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Rate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<VAT, VATDto>(vAT);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VATExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _vATRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.RateMin, input.RateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<VAT>, List<VATExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "VATs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VATExcelDownloadTokenCacheItem { Token = token },
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