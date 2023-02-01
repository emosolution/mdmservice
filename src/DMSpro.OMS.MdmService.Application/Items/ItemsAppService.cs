using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.UOMGroupDetails;
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
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.Items
{

    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService : ApplicationService, IItemsAppService
    {
        private readonly IDistributedCache<ItemExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemRepository _itemRepository;
        private readonly ItemManager _itemManager;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;
        private readonly IRepository<VAT, Guid> _vATRepository;
        private readonly IRepository<UOMGroup, Guid> _uOMGroupRepository;
        private readonly IRepository<UOMGroupDetail, Guid> _uOMGroupDetailRepository;
        private readonly IRepository<ItemAttributeValue, Guid> _itemAttributeValueRepository;

        public ItemsAppService(IItemRepository itemRepository, ItemManager itemManager, IDistributedCache<ItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SystemData, Guid> systemDataRepository, IRepository<VAT, Guid> vATRepository, IRepository<UOMGroup, Guid> uOMGroupRepository, IRepository<UOMGroupDetail, Guid> uOMGroupDetailRepository, IRepository<ItemAttributeValue, Guid> itemAttributeValueRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemRepository = itemRepository;
            _itemManager = itemManager; _systemDataRepository = systemDataRepository;
            _vATRepository = vATRepository;
            _uOMGroupRepository = uOMGroupRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _itemAttributeValueRepository = itemAttributeValueRepository;
        }

        public virtual async Task<PagedResultDto<ItemWithNavigationPropertiesDto>> GetListAsync(GetItemsInput input)
        {
            var totalCount = await _itemRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);
            var items = await _itemRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemWithNavigationProperties>, List<ItemWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemWithNavigationProperties, ItemWithNavigationPropertiesDto>
                (await _itemRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ItemDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Item, ItemDto>(await _itemRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupDetailLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMGroupDetailRepository.GetQueryableAsync());

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOMGroupDetail>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroupDetail>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.Items.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemDto> CreateAsync(ItemCreateDto input)
        {
            if (input.ItemTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.VatId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["VAT"]]);
            }
            if (input.UomGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.InventoryUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }
            if (input.PurUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }
            if (input.SalesUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }

            var item = await _itemManager.CreateAsync(
            input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice, input.Active, input.ManageItemBy, input.CanUpdate, input.ExpiredType, input.ExpiredValue, input.IssueMethod
            );

            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
        {
            if (input.ItemTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.VatId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["VAT"]]);
            }
            if (input.UomGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.InventoryUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }
            if (input.PurUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }
            if (input.SalesUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroupDetail"]]);
            }

            var item = await _itemManager.UpdateAsync(
            id,
            input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice, input.Active, input.ManageItemBy, input.CanUpdate, input.ExpiredType, input.ExpiredValue, input.IssueMethod, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.ERPCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Item>, List<ItemExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Items.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemExcelDownloadTokenCacheItem { Token = token },
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