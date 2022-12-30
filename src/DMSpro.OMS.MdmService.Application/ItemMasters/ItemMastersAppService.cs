using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.SystemDatas;
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
using DMSpro.OMS.MdmService.ItemMasters;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.ItemMasters
{

    [Authorize(MdmServicePermissions.ItemMasters.Default)]
    public class ItemMastersAppService : ApplicationService, IItemMastersAppService
    {
        private readonly IDistributedCache<ItemMasterExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly ItemMasterManager _itemMasterManager;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;
        private readonly IRepository<VAT, Guid> _vATRepository;
        private readonly IRepository<UOMGroup, Guid> _uOMGroupRepository;
        private readonly IRepository<UOM, Guid> _uOMRepository;
        private readonly IRepository<ProdAttributeValue, Guid> _prodAttributeValueRepository;

        public ItemMastersAppService(IItemMasterRepository itemMasterRepository, ItemMasterManager itemMasterManager, IDistributedCache<ItemMasterExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SystemData, Guid> systemDataRepository, IRepository<VAT, Guid> vATRepository, IRepository<UOMGroup, Guid> uOMGroupRepository, IRepository<UOM, Guid> uOMRepository, IRepository<ProdAttributeValue, Guid> prodAttributeValueRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemMasterRepository = itemMasterRepository;
            _itemMasterManager = itemMasterManager; _systemDataRepository = systemDataRepository;
            _vATRepository = vATRepository;
            _uOMGroupRepository = uOMGroupRepository;
            _uOMRepository = uOMRepository;
            _prodAttributeValueRepository = prodAttributeValueRepository;
        }

        public virtual async Task<PagedResultDto<ItemMasterWithNavigationPropertiesDto>> GetListAsync(GetItemMastersInput input)
        {
            var totalCount = await _itemMasterRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.Purchasble, input.Saleable, input.Inventoriable, input.Active, input.ManageType, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.BasePriceMin, input.BasePriceMax, input.ItemTypeId, input.VATId, input.UOMGroupId, input.InventoryUnitId, input.PurUnitId, input.SalesUnit, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);
            var items = await _itemMasterRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.Purchasble, input.Saleable, input.Inventoriable, input.Active, input.ManageType, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.BasePriceMin, input.BasePriceMax, input.ItemTypeId, input.VATId, input.UOMGroupId, input.InventoryUnitId, input.PurUnitId, input.SalesUnit, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemMasterWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemMasterWithNavigationProperties>, List<ItemMasterWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemMasterWithNavigationProperties, ItemMasterWithNavigationPropertiesDto>
                (await _itemMasterRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemMasterRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemMaster>, IEnumerable<ItemMasterDto>>(results.data.Cast<ItemMaster>());
            
            return results;
                
        }
        public virtual async Task<ItemMasterDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemMaster, ItemMasterDto>(await _itemMasterRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ValueCode != null &&
                         x.ValueCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input)
        {
            var query = (await _vATRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<VAT>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VAT>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(MdmServicePermissions.ItemMasters.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemMasterRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemMasters.Create)]
        public virtual async Task<ItemMasterDto> CreateAsync(ItemMasterCreateDto input)
        {
            if (input.ItemTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.VATId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["VAT"]]);
            }
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.InventoryUnitId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.PurUnitId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.SalesUnit == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemMaster = await _itemMasterManager.CreateAsync(
            input.ItemTypeId, input.VATId, input.UOMGroupId, input.InventoryUnitId, input.PurUnitId, input.SalesUnit, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.Purchasble, input.Saleable, input.Inventoriable, input.Active, input.ManageType, input.ExpiredType, input.ExpiredValue, input.IssueMethod, input.CanUpdate, input.BasePrice
            );

            return ObjectMapper.Map<ItemMaster, ItemMasterDto>(itemMaster);
        }

        [Authorize(MdmServicePermissions.ItemMasters.Edit)]
        public virtual async Task<ItemMasterDto> UpdateAsync(Guid id, ItemMasterUpdateDto input)
        {
            if (input.ItemTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.VATId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["VAT"]]);
            }
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.InventoryUnitId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.PurUnitId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.SalesUnit == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var itemMaster = await _itemMasterManager.UpdateAsync(
            id,
            input.ItemTypeId, input.VATId, input.UOMGroupId, input.InventoryUnitId, input.PurUnitId, input.SalesUnit, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.Purchasble, input.Saleable, input.Inventoriable, input.Active, input.ManageType, input.ExpiredType, input.ExpiredValue, input.IssueMethod, input.CanUpdate, input.BasePrice, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemMaster, ItemMasterDto>(itemMaster);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemMasterExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemMasterRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.Purchasble, input.Saleable, input.Inventoriable, input.Active, input.ManageType, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.BasePriceMin, input.BasePriceMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemMaster>, List<ItemMasterExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemMasters.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemMasterExcelDownloadTokenCacheItem { Token = token },
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