using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
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
                var priceListCollection = await _priceListRepository.GetListAsync();
                foreach (var priceList in priceListCollection)
                {
                    PriceListDetail priceListDetailObj = new()
                    {
                        PriceListId = priceList.Id,
                        Description = "",
                        ItemId = item.Id,
                        UOMId = item.InventoryUOMId,
                        BasedOnPrice = item.BasePrice,
                        Price = item.BasePrice,
                    };
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

                    await _priceListDetailRepository.InsertAsync(priceListDetailObj);
                }
            }

            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
        {
            await CheckCanBeUpdated(id);

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

            return ObjectMapper.Map<Item, ItemDto>(item);
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
    }
}
