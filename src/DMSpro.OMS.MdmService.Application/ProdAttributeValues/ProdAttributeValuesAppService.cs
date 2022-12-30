using DMSpro.OMS.MdmService.ProductAttributes;
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
using DMSpro.OMS.MdmService.ProdAttributeValues;
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
namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ProdAttributeValues.Default)]
    public class ProdAttributeValuesAppService : ApplicationService, IProdAttributeValuesAppService
    {
        private readonly IDistributedCache<ProdAttributeValueExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IProdAttributeValueRepository _prodAttributeValueRepository;
        private readonly ProdAttributeValueManager _prodAttributeValueManager;
        private readonly IRepository<ProductAttribute, Guid> _productAttributeRepository;

        public ProdAttributeValuesAppService(IProdAttributeValueRepository prodAttributeValueRepository, ProdAttributeValueManager prodAttributeValueManager, IDistributedCache<ProdAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ProductAttribute, Guid> productAttributeRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _prodAttributeValueRepository = prodAttributeValueRepository;
            _prodAttributeValueManager = prodAttributeValueManager; _productAttributeRepository = productAttributeRepository;
        }

        public virtual async Task<PagedResultDto<ProdAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetProdAttributeValuesInput input)
        {
            var totalCount = await _prodAttributeValueRepository.GetCountAsync(input.FilterText, input.AttrValName, input.ProdAttributeId, input.ParentProdAttributeValueId);
            var items = await _prodAttributeValueRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AttrValName, input.ProdAttributeId, input.ParentProdAttributeValueId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProdAttributeValueWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProdAttributeValueWithNavigationProperties>, List<ProdAttributeValueWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ProdAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ProdAttributeValueWithNavigationProperties, ProdAttributeValueWithNavigationPropertiesDto>
                (await _prodAttributeValueRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _prodAttributeValueRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ProdAttributeValue>, IEnumerable<ProdAttributeValueDto>>(results.data.Cast<ProdAttributeValue>());
            
            return results;
                
        }
        public virtual async Task<ProdAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ProdAttributeValue, ProdAttributeValueDto>(await _prodAttributeValueRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProductAttributeLookupAsync(LookupRequestDto input)
        {
            var query = (await _productAttributeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrName != null &&
                         x.AttrName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ProductAttribute>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProductAttribute>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _prodAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ProdAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProdAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ProdAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _prodAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ProdAttributeValues.Create)]
        public virtual async Task<ProdAttributeValueDto> CreateAsync(ProdAttributeValueCreateDto input)
        {
            if (input.ProdAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ProductAttribute"]]);
            }

            var prodAttributeValue = await _prodAttributeValueManager.CreateAsync(
            input.ProdAttributeId, input.ParentProdAttributeValueId, input.AttrValName
            );

            return ObjectMapper.Map<ProdAttributeValue, ProdAttributeValueDto>(prodAttributeValue);
        }

        [Authorize(MdmServicePermissions.ProdAttributeValues.Edit)]
        public virtual async Task<ProdAttributeValueDto> UpdateAsync(Guid id, ProdAttributeValueUpdateDto input)
        {
            if (input.ProdAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ProductAttribute"]]);
            }

            var prodAttributeValue = await _prodAttributeValueManager.UpdateAsync(
            id,
            input.ProdAttributeId, input.ParentProdAttributeValueId, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ProdAttributeValue, ProdAttributeValueDto>(prodAttributeValue);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProdAttributeValueExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _prodAttributeValueRepository.GetListAsync(input.FilterText, input.AttrValName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ProdAttributeValue>, List<ProdAttributeValueExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ProdAttributeValues.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ProdAttributeValueExcelDownloadTokenCacheItem { Token = token },
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