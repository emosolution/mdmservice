using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Collections.Generic;
using System.Linq;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Lib.Parser;

namespace DMSpro.OMS.MdmService.ItemAttributes
{

    [Authorize(MdmServicePermissions.ItemAttributes.Default)]
    public partial class ItemAttributesAppService
    {
        public virtual async Task<ItemAttributeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttribute, ItemAttributeDto>(await _itemAttributeRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Delete)]
        public virtual async Task<LoadResult> DeleteAsync()
        {
            var lastActiveAttribute = await GetLastActiveAttribute();
            if (lastActiveAttribute == null)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributesAppService:551"], code: "0");
            }
            var canReset = await CheckCanReset();
            if (!canReset)
            {
                if (lastActiveAttribute.HierarchyLevel == null &&
                    await _itemAttributeValueRepository.AnyAsync(x => x.ItemAttributeId == lastActiveAttribute.Id))
                {
                    throw new UserFriendlyException(message: L["Error:ItemAttributesAppService:552"], code: "0");
                }

            }
            ResetItemAttribute(lastActiveAttribute);
            await _itemAttributeRepository.UpdateAsync(lastActiveAttribute);
            return await GetListDevextremesAsync(canReset: canReset);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<LoadResult> CreateFlatAsync(ItemAttributeCreateDto input)
        {
            CheckInput(input.AttrName);

            var firstInactiveAttribute = await GetAttributeForCreation();
            firstInactiveAttribute.HierarchyLevel = null;
            firstInactiveAttribute.Active = true;
            firstInactiveAttribute.AttrName = input.AttrName;
            await _itemAttributeRepository.UpdateAsync(firstInactiveAttribute);
            return await GetListDevextremesAsync(canReset: null);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Create)]
        public virtual async Task<LoadResult> CreateHierarchyAsync(ItemAttributeCreateDto input)
        {
            CheckInput(input.AttrName);

            var firstInactiveAttribute = await GetAttributeForCreation();
            int hierarchyLevel = 0;
            var currentBottomMostHierarchicalAttribute = 
                (await _itemAttributeRepository.GetListAsync(
                    x => x.Active == true && x.HierarchyLevel != null))
                    .OrderByDescending(x => x.HierarchyLevel).FirstOrDefault();
            if (currentBottomMostHierarchicalAttribute != null)
            {
                hierarchyLevel = 
                    currentBottomMostHierarchicalAttribute.HierarchyLevel.Value + 1;
            }
            firstInactiveAttribute.HierarchyLevel = hierarchyLevel;
            firstInactiveAttribute.Active = true;
            firstInactiveAttribute.AttrName = input.AttrName;
            await _itemAttributeRepository.UpdateAsync(firstInactiveAttribute);
            return await GetListDevextremesAsync(canReset: null);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Edit)]
        public virtual async Task<LoadResult> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
        {
            CheckInput(input.AttrName);

            var attribute = await _itemAttributeRepository.GetAsync(id);
            attribute.AttrName = input.AttrName;
            attribute.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _itemAttributeRepository.UpdateAsync(attribute);
            return await GetListDevextremesAsync(canReset: null);
        }

        [Authorize(MdmServicePermissions.ItemAttributes.Delete)]
        public virtual async Task<LoadResult> ResetAsync()
        {
            if (!(await CheckCanReset()))
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributesAppService:550"], code: "0");
            }
            var allAttributes = await _itemAttributeRepository.GetListAsync();
            foreach (var attribute in allAttributes)
            {
                ResetItemAttribute(attribute);
            }
            await _itemAttributeRepository.UpdateManyAsync(allAttributes);
            return await GetListDevextremesAsync(canReset: true);
        }

        private async Task<ItemAttribute> GetFirstInactiveAttribute()
        {
            return (await _itemAttributeRepository.GetListAsync(x => x.Active == false))
                    .OrderBy(x => x.AttrNo).FirstOrDefault();
        }

        private async Task<ItemAttribute> GetLastActiveAttribute()
        {
            return (await _itemAttributeRepository.GetListAsync(x => x.Active == true))
                    .OrderByDescending(x => x.AttrNo).FirstOrDefault();
        }

        private async Task<LoadResult> GetListDevextremesAsync(
            bool? canReset = null)
        {
            DataLoadOptionDevextreme inputDev = new() { Take = 20, Skip = 0, };
            var result = await base.GetListDevextremesAsync(inputDev);
            canReset ??= await CheckCanReset();
            var summaryInfo = new Dictionary<string, string>()
            {
                { "canReset", canReset.ToString() },
            };
            result.summary = new[] { summaryInfo };
            return result;
        }

        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var results = await base.GetListDevextremesAsync(inputDev);
            bool canReset = await CheckCanReset();
            var summaryInfo = new Dictionary<string, string>()
            {
                { "canReset", canReset.ToString() },
            };
            results.summary = new[] { summaryInfo };
            return results;
        }

        private async Task<bool> CheckCanReset()
        {
            return !(await _itemAttributeValueRepository.AnyAsync());
        }

        private static void ResetItemAttribute(ItemAttribute itemAttribute)
        {
            itemAttribute.Active = false;
            itemAttribute.AttrName =
                $"{ItemAttributeConsts.DefaultAttributeNamePrefix}{itemAttribute.AttrNo}";
            itemAttribute.HierarchyLevel = null;
        }

        private static void CheckInput(string attrName)
        {
            Check.NotNullOrWhiteSpace(attrName, nameof(attrName));
            Check.Length(attrName, nameof(attrName),
                ItemAttributeConsts.AttrNameMaxLength, ItemAttributeConsts.AttrNameMinLength);
        }

        private async Task<ItemAttribute> GetAttributeForCreation()
        {
            var firstInactiveAttribute = await GetFirstInactiveAttribute();
            if (firstInactiveAttribute == null)
            {
                throw new UserFriendlyException(message: L["Error:ItemAttributesAppService:553"], code: "0");
            }
            return firstInactiveAttribute;
        }
    }
}