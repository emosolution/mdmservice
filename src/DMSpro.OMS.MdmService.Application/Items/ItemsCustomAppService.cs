﻿using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService
    {
        [Authorize(MdmServicePermissions.Items.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await CheckCanBeUpdated(id);
            await _itemRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemDto> CreateAsync(ItemCreateDto input)
        {
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
            (var numberingConfig, var hoCompanyId) = await GetCodeFromNumberingConfig();
            var item = await _itemManager.CreateAsync(
                input.VatId, input.UomGroupId, input.InventoryUOMId,
                input.PurUOMId, input.SalesUOMId,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id,
                numberingConfig.SuggestedCode, input.Name, input.ShortName, input.erpCode, input.Barcode,
                input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice,
                input.Active, input.ManageItemBy,
                input.PurUnitRate, input.SalesUnitRate,
                input.ItemType, input.ExpiredType, input.ExpiredValue, input.IssueMethod);

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
                        decimal addValue = 0;
                        if (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE)
                        {
                            addValue = price * (priceList.ArithmeticFactor ?? 0) / 100;
                        }
                        else addValue = priceList.ArithmeticFactor ?? 0;

                        switch (priceList.ArithmeticOperation)
                        {
                            case ArithmeticOperator.ADD:
                                priceListDetailObj.Price = price + addValue;
                                break;
                            case ArithmeticOperator.SUBTRACT:
                                priceListDetailObj.Price = price - addValue;
                                break;
                            default:
                                break;
                        }

                        priceListDetails.Add(priceListDetailObj);
                    }
                }
                await _priceListDetailRepository.InsertManyAsync(priceListDetails);
            }
            await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
                ItemConsts.NumberingConfigObjectType, hoCompanyId, numberingConfig.CurrentNumber);
            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
        {
            await CheckCanBeUpdated(id);

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
            var item = await _itemManager.UpdateAsync(
                id,
                input.VatId, input.UomGroupId, input.InventoryUOMId,
                input.PurUOMId, input.SalesUOMId,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id,
                input.Name, input.ShortName, input.erpCode, input.Barcode,
                input.IsPurchasable, input.IsSaleable, input.IsInventoriable, input.BasePrice,
                input.Active, input.ManageItemBy,
                input.PurUnitRate, input.SalesUnitRate,
                input.ItemType, input.ExpiredType, input.ExpiredValue, input.IssueMethod,
                input.ConcurrencyStamp
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

        public virtual async Task<ItemDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Item, ItemDto>(await _itemRepository.GetAsync(id));
        }

        private async Task CheckCanBeUpdated(Guid id)
        {
            var record = await _itemRepository.GetAsync(id);
            if (!record.CanUpdate)
            {
                throw new UserFriendlyException(message: L["Error:ItemsAppService:550"], code: "0");
            }
            bool canBeUpdated = await _itemsInternalAppService.CheckCanBeUpdatedAsync(id);
            if (!canBeUpdated)
            {
                record.CanUpdate = false;
                await _itemRepository.UpdateAsync(record);
                throw new UserFriendlyException(message: L["Error:ItemsAppService:550"], code: "0");
            }
        }

        public async Task<ItemProfileDto> GetItemProfileAsync(Guid id)
        {
            Item item = await _itemRepository.GetAsync(id);
            List<ItemAttachment> attachments = (await _itemAttachmentRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemImage> images = (await _itemImageRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            var result = new ItemProfileDto()
            {
                Item = ObjectMapper.Map<Item, ItemDto>(item),
                Attachments = ObjectMapper.Map<List<ItemAttachment>, List<ItemAttachmentDto>>(attachments),
                Images = ObjectMapper.Map<List<ItemImage>, List<ItemImageDto>>(images),
            };
            return result;
        }

        private async Task<(NumberingConfigDetailDto, Guid)> GetCodeFromNumberingConfig()
        {
            var hoCompany = await _companyRepository.GetAsync(x => x.IsHO == true);
            var dto = 
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
                    ItemConsts.NumberingConfigObjectType, hoCompany.Id);
            string code = dto.SuggestedCode;
            await CheckCodeUniqueness(code);
            return (dto, hoCompany.Id);
        }
    }
}
