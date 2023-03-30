using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemAttributeValues;
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
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Items
{

    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService
    {
        public virtual async Task<PagedResultDto<ItemWithNavigationPropertiesDto>> GetListAsync(GetItemsInput input)
        {
            var totalCount = await _itemRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.erpCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.PurUnitRateMin, input.PurUnitRateMax, input.SalesUnitRateMin, input.SalesUnitRateMax, input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id);
            var items = await _itemRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.erpCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.PurUnitRateMin, input.PurUnitRateMax, input.SalesUnitRateMin, input.SalesUnitRateMax, input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttributeValue>, List<LookupDto<Guid>>>(lookupData)
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
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.PurUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.SalesUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            await CheckCodeUniqueness(input.Code);
            var item = await _itemManager.CreateAsync(
            input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.erpCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice, input.Active, input.ManageItemBy, input.CanUpdate, input.PurUnitRate, input.SalesUnitRate, input.ExpiredType, input.ExpiredValue, input.IssueMethod
            );

            //Add Item to Price List
            if (await _priceListRepository.CountAsync() > 0)
            {
                List<PriceListDetail> priceListDetails = new();
                var priceListCollection = await _priceListRepository.GetListAsync();

                foreach (var priceList in priceListCollection)
                {
                    foreach (var uom in await _uOMGroupDetailRepository.GetListAsync(x => x.UOMGroupId == item.UomGroupId))
                    {
                        var price = item.BasePrice * uom.BaseQty;
                        PriceListDetail priceListDetailObj = new()
                        {
                            PriceListId = priceList.Id,
                            Description = "",
                            ItemId = item.Id,
                            UOMId = uom.AltUOMId,
                            BasedOnPrice = price,
                            Price = price,
                        };
                        switch (priceList.ArithmeticOperation)
                        {
                            case ArithmeticOperator.ADD:
                                priceListDetailObj.Price = price + priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                                break;
                            case ArithmeticOperator.SUBTRACT:
                                priceListDetailObj.Price = price - priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                                break;
                            default:
                                break;
                        }

                        priceListDetails.Add(priceListDetailObj);
                    }
                }
                await _priceListDetailRepository.InsertManyAsync(priceListDetails);
            }

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
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.PurUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.SalesUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            await CheckCodeUniqueness(input.Code, id);
            var item = await _itemManager.UpdateAsync(
            id,
            input.ItemTypeId, input.VatId, input.UomGroupId, input.InventoryUOMId, input.PurUOMId, input.SalesUOMId, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.Code, input.Name, input.ShortName, input.erpCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice, input.Active, input.ManageItemBy, input.CanUpdate, input.PurUnitRate, input.SalesUnitRate, input.ExpiredType, input.ExpiredValue, input.IssueMethod, input.ConcurrencyStamp
            );

            //Update Price to PriceList
            if (await _priceListRepository.CountAsync() > 0)
            {
                var priceListCollection = await _priceListRepository.GetListAsync();
                foreach (var priceList in priceListCollection)
                {
                    var priceListDetailObj = await _priceListDetailRepository.FirstOrDefaultAsync(x => x.ItemId == item.Id && x.UOMId == item.InventoryUOMId);
                    if (priceListDetailObj is not null)
                    {
                        priceListDetailObj.BasedOnPrice = item.BasePrice;
                        priceListDetailObj.Price = item.BasePrice;

                        switch (priceList.ArithmeticOperation)
                        {
                            case ArithmeticOperator.ADD:
                                priceListDetailObj.Price = item.BasePrice + priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                                break;
                            case ArithmeticOperator.SUBTRACT:
                                priceListDetailObj.Price = item.BasePrice - priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                                break;
                            default:
                                break;
                        }
                    }

                    await _priceListDetailRepository.UpdateAsync(priceListDetailObj);
                }
            }

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

            var items = await _itemRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.erpCode, input.Barcode, input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePriceMin, input.BasePriceMax, input.Active, input.ManageItemBy, input.ExpiredType, input.ExpiredValueMin, input.ExpiredValueMax, input.IssueMethod, input.CanUpdate, input.PurUnitRateMin, input.PurUnitRateMax, input.SalesUnitRateMin, input.SalesUnitRateMax);
            var result = items.Select(item => new
            {
                Code = item.Item.Code,
                Name = item.Item.Name,
                ShortName = item.Item.ShortName,
                erpCode = item.Item.erpCode,
                Barcode = item.Item.Barcode,
                IsPurchasable = item.Item.IsPurchasable,
                IsSaleable = item.Item.IsSaleable,
                IsInventoriable = item.Item.IsInventoriable,
                BasePrice = item.Item.BasePrice,
                Active = item.Item.Active,
                ManageItemBy = item.Item.ManageItemBy,
                ExpiredType = item.Item.ExpiredType,
                ExpiredValue = item.Item.ExpiredValue,
                IssueMethod = item.Item.IssueMethod,
                CanUpdate = item.Item.CanUpdate,
                PurUnitRate = item.Item.PurUnitRate,
                SalesUnitRate = item.Item.SalesUnitRate,

                SystemDataCode = item.SystemData?.Code,
                VATCode = item.VAT?.Code,
                UOMGroupCode = item.UOMGroup?.Code,
                InventoryUnit = item.InventoryUnit?.Code,
                PurUnit = item.PurUnit?.Code,
                SalesUnit = item.SalesUnit?.Code,
                Attr0 = item.Attr0?.AttrValName,
                Attr1 = item.Attr1?.AttrValName,
                Attr2 = item.Attr2?.AttrValName,
                Attr3 = item.Attr3?.AttrValName,
                Attr4 = item.Attr4?.AttrValName,
                Attr5 = item.Attr5?.AttrValName,
                Attr6 = item.Attr6?.AttrValName,
                Attr7 = item.Attr7?.AttrValName,
                Attr8 = item.Attr8?.AttrValName,
                Attr9 = item.Attr9?.AttrValName,
                Attr10 = item.Attr10?.AttrValName,
                Attr11 = item.Attr11?.AttrValName,
                Attr12 = item.Attr12?.AttrValName,
                Attr13 = item.Attr13?.AttrValName,
                Attr14 = item.Attr14?.AttrValName,
                Attr15 = item.Attr15?.AttrValName,
                Attr16 = item.Attr16?.AttrValName,
                Attr17 = item.Attr17?.AttrValName,
                Attr18 = item.Attr18?.AttrValName,
                Attr19 = item.Attr19?.AttrValName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(result);
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