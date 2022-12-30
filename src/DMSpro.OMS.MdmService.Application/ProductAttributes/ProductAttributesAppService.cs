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
using DMSpro.OMS.MdmService.ProductAttributes;
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
namespace DMSpro.OMS.MdmService.ProductAttributes
{

    [Authorize(MdmServicePermissions.ProductAttributes.Default)]
    public class ProductAttributesAppService : ApplicationService, IProductAttributesAppService
    {
        private readonly IDistributedCache<ProductAttributeExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ProductAttributeManager _productAttributeManager;

        public ProductAttributesAppService(IProductAttributeRepository productAttributeRepository, ProductAttributeManager productAttributeManager, IDistributedCache<ProductAttributeExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeManager = productAttributeManager;
        }

        public virtual async Task<PagedResultDto<ProductAttributeDto>> GetListAsync(GetProductAttributesInput input)
        {
            var totalCount = await _productAttributeRepository.GetCountAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsProductCategory);
            var items = await _productAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsProductCategory, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductAttributeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeDto>>(items)
            };
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _productAttributeRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ProductAttribute>, IEnumerable<ProductAttributeDto>>(results.data.Cast<ProductAttribute>());
            
            return results;
                
        }
        public virtual async Task<ProductAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ProductAttribute, ProductAttributeDto>(await _productAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ProductAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productAttributeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ProductAttributes.Create)]
        public virtual async Task<ProductAttributeDto> CreateAsync(ProductAttributeCreateDto input)
        {

            var productAttribute = await _productAttributeManager.CreateAsync(
            input.AttrNo, input.AttrName, input.Active, input.IsProductCategory, input.HierarchyLevel
            );

            return ObjectMapper.Map<ProductAttribute, ProductAttributeDto>(productAttribute);
        }

        [Authorize(MdmServicePermissions.ProductAttributes.Edit)]
        public virtual async Task<ProductAttributeDto> UpdateAsync(Guid id, ProductAttributeUpdateDto input)
        {

            var productAttribute = await _productAttributeManager.UpdateAsync(
            id,
            input.AttrNo, input.AttrName, input.Active, input.IsProductCategory, input.HierarchyLevel, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ProductAttribute, ProductAttributeDto>(productAttribute);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductAttributeExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _productAttributeRepository.GetListAsync(input.FilterText, input.AttrNoMin, input.AttrNoMax, input.AttrName, input.HierarchyLevelMin, input.HierarchyLevelMax, input.Active, input.IsProductCategory);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ProductAttributes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ProductAttributeExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
        // TODO hovanbuu: Implement permission to seed Product Attribute when a new tenant is created
        // TODO hvoanbuu: Create controller for front end to manually seed ProdAttribute in case of failed auto seeding.
    }
}