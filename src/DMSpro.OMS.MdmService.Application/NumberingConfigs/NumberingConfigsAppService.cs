using DMSpro.OMS.MdmService.SystemDatas;
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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.NumberingConfigs;
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
namespace DMSpro.OMS.MdmService.NumberingConfigs
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public class NumberingConfigsAppService : ApplicationService, INumberingConfigsAppService
    {
        private readonly IDistributedCache<NumberingConfigExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly NumberingConfigManager _numberingConfigManager;
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;

        public NumberingConfigsAppService(INumberingConfigRepository numberingConfigRepository, NumberingConfigManager numberingConfigManager, IDistributedCache<NumberingConfigExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Company, Guid> companyRepository, IRepository<SystemData, Guid> systemDataRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _numberingConfigRepository = numberingConfigRepository;
            _numberingConfigManager = numberingConfigManager; _companyRepository = companyRepository;
            _systemDataRepository = systemDataRepository;
        }

        public virtual async Task<PagedResultDto<NumberingConfigWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigsInput input)
        {
            var totalCount = await _numberingConfigRepository.GetCountAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax, input.CompanyId, input.SystemDataId);
            var items = await _numberingConfigRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax, input.CompanyId, input.SystemDataId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<NumberingConfigWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<NumberingConfigWithNavigationProperties>, List<NumberingConfigWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _numberingConfigRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<NumberingConfig>, IEnumerable<NumberingConfigDto>>(results.data.Cast<NumberingConfig>());
            
            return results;
                
        }

        public virtual async Task<NumberingConfigWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfigWithNavigationProperties, NumberingConfigWithNavigationPropertiesDto>
                (await _numberingConfigRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<NumberingConfigDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(await _numberingConfigRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _numberingConfigRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Create)]
        public virtual async Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input)
        {

            var numberingConfig = await _numberingConfigManager.CreateAsync(
            input.CompanyId, input.SystemDataId, input.StartNumber, input.Prefix, input.Suffix, input.Length
            );

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {

            var numberingConfig = await _numberingConfigManager.UpdateAsync(
            id,
            input.CompanyId, input.SystemDataId, input.StartNumber, input.Prefix, input.Suffix, input.Length, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _numberingConfigRepository.GetListAsync(input.FilterText, input.StartNumberMin, input.StartNumberMax, input.Prefix, input.Suffix, input.LengthMin, input.LengthMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<NumberingConfig>, List<NumberingConfigExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "NumberingConfigs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new NumberingConfigExcelDownloadTokenCacheItem { Token = token },
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