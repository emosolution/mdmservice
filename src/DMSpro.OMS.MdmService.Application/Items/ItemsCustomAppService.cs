using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.Items
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemsAppService
    {
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

        public async Task<string> GetInfoForSOAsync(Guid companyId, DateTime lastApiDate)
        {
            var updateRequired = await CheckUpdateRequired(lastApiDate);
            if (!updateRequired)
            {
                return $"{{updateRequired: false}}";
            }
            List<string> resultParts = new()
            {
                $"{{updateRequired: true}}",
            };
            DateTime now = DateTime.Now;
            await CheckCompany(companyId, now);
            List<SalesOrgHierarchy> sellingZones = await GetAllSellingZones(companyId, now);
            List<Guid> zoneIds = await sellingZones.AsQueryable().Select(x => x.Id)
                .Distinct().ToListAsync();
            string itemInfo = await GetItemInfoForSOAsync(zoneIds);
            string customerInfo = await GetCustomerInfoForSOAsync(zoneIds);

        }

        private async Task<string> GetCustomerInfoForSOAsync(List<Guid> zoneIds)
        {

        }

        public async Task<string> GetItemInfoForSOAsync(List<Guid> zoneIds)
        {
            List<Guid> itemGroupIds = await GetAllItemGroupIds(zoneIds);
            List<ItemGroup> itemGroups = await GetAllItemGroups(itemGroupIds);
            (Dictionary<string, List<string>> uomGroupDictionary,
                Dictionary<string, ItemSOPODto> itemDictionary,
                Dictionary<string, List<string>> altUOMDictionary,
                List<string> allAltUomIds,
                List<Guid> vatIds) = await GetItemDetails(itemGroups);
            Dictionary<string, VATSOPODto> vatDictionary = await GetVatDetails(vatIds);
            Dictionary<string, UOMSOPODto> uomDictionary = await GetUOMDetails(allAltUomIds);

            return resultParts.JoinAsString(",");
        }

        private async Task<Company> CheckCompany(Guid companyId, DateTime now)
        {
            try
            {
                var result = await _companyRepository.GetAsync(x => x.Id == companyId &&
                    x.Active == true && x.EffectiveDate < now && (x.EndDate == null || x.EndDate > now));
                return result;
            }
            catch (EntityNotFoundException)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:550"], code: "1");
            }
        }

        private async Task<List<SalesOrgHierarchy>> GetAllSellingZones(Guid companyId, DateTime now)
        {
            var companiesInZone = await _companyInZoneRepository.GetListAsync(x => x.CompanyId == companyId &&
                x.IsBase == true && x.EffectiveDate < now && (x.EndDate == null || x.EndDate > now));
            var zones = companiesInZone.AsQueryable().Select(x => x.SalesOrgHierarchy);
            var result = await zones.Where(x => x.Active && x.IsSellingZone == true).Distinct().ToListAsync();
            if (result.Count < 1)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:551"], code: "1");
            }
            return result;
        }

        private async Task<List<Guid>> GetAllItemGroupIds(List<Guid> zoneIds)
        {
            var itemGroupsInZone = await _itemGroupInZoneRepository.GetListAsync(x => zoneIds.Contains(x.SellingZoneId));
            var result = await itemGroupsInZone.AsQueryable().Select(x => x.ItemGroupId).Distinct().ToListAsync();
            return result;
        }

        private async Task<List<ItemGroup>> GetAllItemGroups(List<Guid> itemGroupIds)
        {
            var itemGroups = await _itemGroupRepository.GetListAsync(x => itemGroupIds.Contains(x.Id) &&
                x.Status == GroupStatus.RELEASED);
            return itemGroups;
        }

        private async Task<List<Item>> GetAllItemIdsFromItemGroup(ItemGroup itemGroup)
        {
            if (itemGroup.Type == GroupType.LIST)
            {
                return await GetAllItemIdsFromItemGroupList(itemGroup.Id);
            }
            else if (itemGroup.Type == GroupType.ATTRIBUTE)
            {
                return await GetAllItemIdsFromItemGroupAttr(itemGroup.Id);
            }
            throw new BusinessException(message: L["Error:ItemsAppService:552"], code: "1");
        }

        private async Task<List<Item>> GetAllItemIdsFromItemGroupList(Guid itemGroupId)
        {
            var itemGroupLists = (await _itemGroupListRepository.GetListAsync(
                x => x.ItemGroupId == itemGroupId));
            List<Guid> itemIds = await itemGroupLists.AsQueryable().Select(x => x.ItemId)
                .Distinct().ToListAsync();
            return await _repository.GetListAsync(x => itemIds.Contains(x.Id));
        }

        private async Task<List<Item>> GetAllItemIdsFromItemGroupAttr(Guid itemGroupId)
        {
            var itemAttribues = (await _itemGroupAttributeRepository.GetListAsync(
                x => x.ItemGroupId == itemGroupId)).AsQueryable();
            var attr0Values = await itemAttribues.Select(x => x.Attr0Id).Distinct().ToListAsync();
            var attr1Values = await itemAttribues.Select(x => x.Attr1Id).Distinct().ToListAsync();
            var attr2Values = await itemAttribues.Select(x => x.Attr2Id).Distinct().ToListAsync();
            var attr3Values = await itemAttribues.Select(x => x.Attr3Id).Distinct().ToListAsync();
            var attr4Values = await itemAttribues.Select(x => x.Attr4Id).Distinct().ToListAsync();
            var attr5Values = await itemAttribues.Select(x => x.Attr5Id).Distinct().ToListAsync();
            var attr6Values = await itemAttribues.Select(x => x.Attr6Id).Distinct().ToListAsync();
            var attr7Values = await itemAttribues.Select(x => x.Attr7Id).Distinct().ToListAsync();
            var attr8Values = await itemAttribues.Select(x => x.Attr8Id).Distinct().ToListAsync();
            var attr9Values = await itemAttribues.Select(x => x.Attr9Id).Distinct().ToListAsync();
            var attr10Values = await itemAttribues.Select(x => x.Attr10Id).Distinct().ToListAsync();
            var attr11Values = await itemAttribues.Select(x => x.Attr11Id).Distinct().ToListAsync();
            var attr12Values = await itemAttribues.Select(x => x.Attr12Id).Distinct().ToListAsync();
            var attr13Values = await itemAttribues.Select(x => x.Attr13Id).Distinct().ToListAsync();
            var attr14Values = await itemAttribues.Select(x => x.Attr14Id).Distinct().ToListAsync();
            var attr15Values = await itemAttribues.Select(x => x.Attr15Id).Distinct().ToListAsync();
            var attr16Values = await itemAttribues.Select(x => x.Attr16Id).Distinct().ToListAsync();
            var attr17Values = await itemAttribues.Select(x => x.Attr17Id).Distinct().ToListAsync();
            var attr18Values = await itemAttribues.Select(x => x.Attr18Id).Distinct().ToListAsync();
            var attr19Values = await itemAttribues.Select(x => x.Attr19Id).Distinct().ToListAsync();
            var items = await _itemRepository.GetListAsync(x => attr0Values.Contains(x.Attr0Id) &&
                attr1Values.Contains(x.Attr1Id) && attr2Values.Contains(x.Attr2Id) &&
                attr3Values.Contains(x.Attr3Id) && attr4Values.Contains(x.Attr4Id) &&
                attr5Values.Contains(x.Attr5Id) && attr6Values.Contains(x.Attr6Id) &&
                attr7Values.Contains(x.Attr7Id) && attr8Values.Contains(x.Attr8Id) &&
                attr9Values.Contains(x.Attr9Id) && attr10Values.Contains(x.Attr10Id) &&
                attr11Values.Contains(x.Attr11Id) && attr12Values.Contains(x.Attr12Id) &&
                attr13Values.Contains(x.Attr13Id) && attr14Values.Contains(x.Attr14Id) &&
                attr15Values.Contains(x.Attr15Id) && attr16Values.Contains(x.Attr16Id) &&
                attr17Values.Contains(x.Attr17Id) && attr18Values.Contains(x.Attr18Id) &&
                attr19Values.Contains(x.Attr19Id));
            return items;
        }

        private async Task<(
            Dictionary<string, List<string>>,
            Dictionary<string, ItemSOPODto>,
            Dictionary<string, List<string>>,
            List<string>,
            List<Guid>)>
            GetItemDetails(List<ItemGroup> itemGroups)
        {
            Dictionary<string, List<string>> itemGroupDictionary = new();
            Dictionary<string, ItemSOPODto> itemDictionary = new();
            Dictionary<string, List<string>> altUOMDictionary = new();
            List<string> allAltUomIds = new();
            List<Guid> vatIds = new();
            foreach (ItemGroup itemGroup in itemGroups)
            {
                List<Item> itemsInGroup = await GetAllItemIdsFromItemGroup(itemGroup);
                List<string> itemInGroupIds = new();
                foreach (Item item in itemsInGroup)
                {
                    string itemId = item.Id.ToString();
                    if (!itemInGroupIds.Contains(itemId))
                    {
                        itemInGroupIds.Add(itemId);
                    }
                    if (!vatIds.Contains(item.VatId))
                    {
                        vatIds.Add(item.VatId);
                    }
                    string uomGroupId = item.UomGroupId.ToString();
                    if (!altUOMDictionary.ContainsKey(uomGroupId))
                    {
                        var altUomIds = await GetAltUOMs(item.UomGroupId);
                        allAltUomIds.AddRange(altUomIds);
                        altUOMDictionary.Add(uomGroupId, altUomIds);
                    }
                    if (itemDictionary.ContainsKey(itemId))
                    {
                        continue;
                    }
                    ItemSOPODto dto = new()
                    {
                        id = itemId,
                        code = item.Code,
                        name = item.Name,
                        basePrice = item.BasePrice,
                        vatId = item.VatId.ToString(),
                        uomGroupId = item.UomGroupId.ToString(),
                        invUomId = item.InventoryUOMId.ToString(),
                        purUomId = item.PurUOMId.ToString(),
                        purRate = item.PurUnitRate,
                        salesUomId = item.SalesUOMId.ToString(),
                        salesRate = item.SalesUnitRate,
                        isPurchasable = item.IsPurchasable,
                        isSalesable = item.IsSaleable,
                    };
                    itemDictionary.Add(itemId, dto);
                }
                string itemGroupId = itemGroup.Id.ToString();
                if (!itemGroupDictionary.ContainsKey(itemGroupId))
                {
                    itemGroupDictionary.Add(itemGroupId, itemInGroupIds);
                }
            }
            return (itemGroupDictionary, itemDictionary,
                altUOMDictionary, allAltUomIds, vatIds);
        }

        private async Task<Dictionary<string, VATSOPODto>> GetVatDetails(List<Guid> vatIds)
        {
            var vats = await _vATRepository.GetListAsync(x => vatIds.Contains(x.Id));
            Dictionary<string, VATSOPODto> result = new();
            foreach (var vat in vats)
            {
                string id = vat.Id.ToString();
                if (result.ContainsKey(id))
                {
                    continue;
                }
                VATSOPODto dto = new()
                {
                    id = id,
                    code = vat.Code,
                    name = vat.Name,
                    rate = vat.Rate,
                };
                result.Add(id, dto);
            }
            return result;
        }

        private async Task<List<string>> GetAltUOMs(Guid uomGroupId)
        {
            var uomGroupDetails = await _uOMGroupDetailRepository.GetListAsync(
                x => x.UOMGroupId == uomGroupId && x.Active == true);
            var result = await uomGroupDetails.AsQueryable()
                .Select(x => x.AltUOMId.ToString()).Distinct().ToListAsync();
            return result;
        }

        private async Task<Dictionary<string, UOMSOPODto>> GetUOMDetails(List<string> allAltUomIds)
        {
            var uoms = await _uOMRepository.GetListAsync(x => allAltUomIds.Contains(x.Id.ToString()));
            Dictionary<string, UOMSOPODto> result = new();
            foreach (var uom in uoms)
            {
                string id = uom.Id.ToString();
                if (result.ContainsKey(id))
                {
                    continue;
                }
                UOMSOPODto dto = new()
                {
                    code = uom.Code,
                    name = uom.Name,
                };
                result.Add(id, dto);
            }
            return result;
        }

        private async Task<bool> CheckUpdateRequired(DateTime lastApiDate)
        {
            throw new NotImplementedException();
        }

        private class ItemSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public decimal basePrice { get; set; }
            public string vatId { get; set; }
            public string uomGroupId { get; set; }
            public string invUomId { get; set; }
            public string purUomId { get; set; }
            public decimal purRate { get; set; }
            public string salesUomId { get; set; }
            public decimal salesRate { get; set; }
            public bool isPurchasable { get; set; }
            public bool isSalesable { get; set; }
            public ItemSOPODto() { }
        }

        private class VATSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public uint rate { get; set; }
            public VATSOPODto() { }
        }

        private class UOMSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public UOMSOPODto() { }
        }

        /*
        public async Task<ItemProfileWithDataDto> GetItemProfileWithData(Guid id)
        {
            Item item = await _itemRepository.GetAsync(id);
            List<ItemAttachment> attachments = (await _itemAttachmentRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemImage> images = (await _itemImageRepository.GetQueryableAsync()).Where(x => x.ItemId == id).ToList();
            List<ItemAttachmentCreateDto> attachmentsWithData = new();
            List<ItemImageCreateDto> imagesWithData = new();

            foreach (ItemAttachment attachment in attachments)
            {
                ItemAttachmentCreateDto dto = new()
                {
                    Description = attachment.Description,
                    Active = attachment.Active,
                    ItemId = attachment.ItemId,
                    File = await _itemAttachmentsAppService.GetFile(attachment.FileId),
                };
                attachmentsWithData.Add(dto);
            }

            foreach (ItemImage image in images)
            {
                ItemImageCreateDto dto = new()
                {
                    Description = image.Description,
                    Active = image.Active,
                    ItemId = image.ItemId,
                    DisplayOrder = image.DisplayOrder,
                    File = await _itemImagesAppService.GetFile(image.FileId),
                };
                imagesWithData.Add(dto);
            }

            var result = new ItemProfileWithDataDto()
            {
                Item = ObjectMapper.Map<Item, ItemDto>(item),
                Attachments = attachmentsWithData,
                Images = imagesWithData,
            };
            return result;
        }
        */
    }
}
