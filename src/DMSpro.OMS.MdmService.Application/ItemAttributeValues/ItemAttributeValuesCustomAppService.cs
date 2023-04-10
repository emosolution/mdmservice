using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public partial class ItemAttributeValuesAppService
    {
        public virtual async Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(await _itemAttributeValueRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await CheckAttributeValueUsedByItem(id);
            await CheckAttributeValueUsedByItemGroup(id);
            var itemAttributeValue = await _itemAttributeValueRepository.GetAsync(id);
            if (await _itemAttributeValueRepository.AnyAsync(x => x.ParentId == itemAttributeValue.Id))
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:552"], code: "1");
            }
            await _itemAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateRootAsync(ItemAttributeValueCreateRootDto input)
        {
            await CheckCodeAndName(input.Code, input.AttrValName);
            var rootItemAttribute =
                (await _itemAttributeRepository.GetListAsync(x => x.HierarchyLevel == 0)).FirstOrDefault();
            if (rootItemAttribute == null)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:555"], code: "1");
            }
            return await InsertAsync(rootItemAttribute.Id, null, input.AttrValName, input.Code);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateHierarchyAsync(ItemAttributeValueCreateHierarchyDto input)
        {
            await CheckCodeAndName(input.Code, input.AttrValName);
            var parentItemAttributeValue =
                await _itemAttributeValueRepository.GetAsync(x => x.Id == input.ParentId);
            var parentItemAttribtute =
                await _itemAttributeRepository.GetAsync(x => x.Id == parentItemAttributeValue.ItemAttributeId);
            if (parentItemAttribtute.HierarchyLevel == null)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:556"], code: "1");
            }
            var itemAttribute = 
                await _itemAttributeRepository.GetAsync(x => x.HierarchyLevel == parentItemAttribtute.HierarchyLevel + 1);
            return await InsertAsync(itemAttribute.Id, input.ParentId, input.AttrValName, input.Code);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateFlatAsync(ItemAttributeValueCreateFlatDto input)
        {
            await CheckCodeAndName(input.Code, input.AttrValName);
            var itemAttribute = await _itemAttributeRepository.GetAsync(input.ItemAttributeId);
            if (itemAttribute.HierarchyLevel != null)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:552"], code: "1");
            }
            return await InsertAsync(input.ItemAttributeId, null, input.AttrValName, input.Code);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Edit)]
        public virtual async Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input)
        {
            await CheckName(input.AttrValName, id);

            var itemAttributeValue = await _itemAttributeValueRepository.GetAsync(id);
            itemAttributeValue.AttrValName = input.AttrValName;
            itemAttributeValue.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _itemAttributeValueRepository.UpdateAsync(itemAttributeValue);
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }

        private async Task CheckAttributeValueUsedByItem(Guid id)
        {
            if (await _itemRepository.AnyAsync(
                x => x.Attr0Id == id || x.Attr1Id == id ||
                x.Attr2Id == id || x.Attr3Id == id ||
                x.Attr4Id == id || x.Attr5Id == id ||
                x.Attr6Id == id || x.Attr7Id == id ||
                x.Attr8Id == id || x.Attr9Id == id ||
                x.Attr10Id == id || x.Attr11Id == id ||
                x.Attr12Id == id || x.Attr13Id == id ||
                x.Attr14Id == id || x.Attr15Id == id ||
                x.Attr16Id == id || x.Attr17Id == id ||
                x.Attr18Id == id || x.Attr19Id == id))
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:550"], code: "0");
            }
        }

        private async Task CheckAttributeValueUsedByItemGroup(Guid id)
        {
            if (await _itemGroupAttributeRepository.AnyAsync(
                x => x.Attr0Id == id || x.Attr1Id == id ||
                x.Attr2Id == id || x.Attr3Id == id ||
                x.Attr4Id == id || x.Attr5Id == id ||
                x.Attr6Id == id || x.Attr7Id == id ||
                x.Attr8Id == id || x.Attr9Id == id ||
                x.Attr10Id == id || x.Attr11Id == id ||
                x.Attr12Id == id || x.Attr13Id == id ||
                x.Attr14Id == id || x.Attr15Id == id ||
                x.Attr16Id == id || x.Attr17Id == id ||
                x.Attr18Id == id || x.Attr19Id == id))
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:551"], code: "0");
            }
        }

        private async Task CheckCodeAndName(string code, string attrName)
        {
            await CheckCode(code);
            await CheckName(attrName);
        }

        private async Task CheckName(string attrName, Guid? id = null)
        {
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName),
                ItemAttributeValueConsts.AttrValNameMaxLength, ItemAttributeValueConsts.AttrValNameMinLength);
            var record = await _itemAttributeValueRepository.FirstOrDefaultAsync(x => x.AttrValName == attrName);
            if (record != null && record.Id != id)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:553"], code: "0");
            }
        }

        private async Task CheckCode(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code),
                ItemAttributeValueConsts.CodeMaxLength, ItemAttributeValueConsts.CodeMinLength);
            if (await _itemAttributeValueRepository.AnyAsync(x => x.AttrValName == code))
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributeValuesAppService:554"], code: "0");
            }
        }

        private async Task<ItemAttributeValueDto> InsertAsync(Guid itemAttributeId,
            Guid? parentId, string attrValName, string code)
        {
            var itemAttributeValue = new ItemAttributeValue(GuidGenerator.Create(),
               itemAttributeId, parentId, attrValName, code);
            await _itemAttributeValueRepository.InsertAsync(itemAttributeValue);
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Default)]
        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _repository.WithDetailsAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            if (inputDev.Group == null)
            {
                results.data =
                    ObjectMapper.Map<IEnumerable<ItemAttributeValue>,
                        IEnumerable<ItemAttributeValueDto>>(results.data.Cast<ItemAttributeValue>());
            }
            List<Guid> itemAttributeValueIds = new();
            foreach (var item in results.data)
            {
                var dto = (ItemAttributeValueDto)item;
                itemAttributeValueIds.Add(dto.Id);
            }
            var children = await _repository.GetListAsync(x => x.ParentId != null &&
                itemAttributeValueIds.Contains((Guid)x.ParentId));
            var itemAttributeValueWithChild = children.Select(x => x.ParentId.ToString()).Distinct().ToArray();
            results.summary = new object[] { itemAttributeValueWithChild };
            return results;
        }
    }
}