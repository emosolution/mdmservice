using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttrManager : DomainService
    {
        private readonly IItemGroupAttrRepository _itemGroupAttrRepository;

        public ItemGroupAttrManager(IItemGroupAttrRepository itemGroupAttrRepository)
        {
            _itemGroupAttrRepository = itemGroupAttrRepository;
        }

        public async Task<ItemGroupAttr> CreateAsync(
        Guid itemGroupId, Guid? attr0, Guid? attr1, Guid? attr2, Guid? attr3, Guid? attr4, Guid? attr5, Guid? attr6, Guid? attr7, Guid? attr8, Guid? attr9, Guid? attr10, Guid? attr11, Guid? attr12, Guid? attr13, Guid? attr14, Guid? attr15, Guid? attr16, Guid? attr17, Guid? attr18, Guid? attr19, bool dummy)
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));

            var itemGroupAttr = new ItemGroupAttr(
             GuidGenerator.Create(),
             itemGroupId, attr0, attr1, attr2, attr3, attr4, attr5, attr6, attr7, attr8, attr9, attr10, attr11, attr12, attr13, attr14, attr15, attr16, attr17, attr18, attr19, dummy
             );

            return await _itemGroupAttrRepository.InsertAsync(itemGroupAttr);
        }

        public async Task<ItemGroupAttr> UpdateAsync(
            Guid id,
            Guid itemGroupId, Guid? attr0, Guid? attr1, Guid? attr2, Guid? attr3, Guid? attr4, Guid? attr5, Guid? attr6, Guid? attr7, Guid? attr8, Guid? attr9, Guid? attr10, Guid? attr11, Guid? attr12, Guid? attr13, Guid? attr14, Guid? attr15, Guid? attr16, Guid? attr17, Guid? attr18, Guid? attr19, bool dummy, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(itemGroupId, nameof(itemGroupId));

            var queryable = await _itemGroupAttrRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemGroupAttr = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemGroupAttr.ItemGroupId = itemGroupId;
            itemGroupAttr.Attr0 = attr0;
            itemGroupAttr.Attr1 = attr1;
            itemGroupAttr.Attr2 = attr2;
            itemGroupAttr.Attr3 = attr3;
            itemGroupAttr.Attr4 = attr4;
            itemGroupAttr.Attr5 = attr5;
            itemGroupAttr.Attr6 = attr6;
            itemGroupAttr.Attr7 = attr7;
            itemGroupAttr.Attr8 = attr8;
            itemGroupAttr.Attr9 = attr9;
            itemGroupAttr.Attr10 = attr10;
            itemGroupAttr.Attr11 = attr11;
            itemGroupAttr.Attr12 = attr12;
            itemGroupAttr.Attr13 = attr13;
            itemGroupAttr.Attr14 = attr14;
            itemGroupAttr.Attr15 = attr15;
            itemGroupAttr.Attr16 = attr16;
            itemGroupAttr.Attr17 = attr17;
            itemGroupAttr.Attr18 = attr18;
            itemGroupAttr.Attr19 = attr19;
            itemGroupAttr.Dummy = dummy;

            itemGroupAttr.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupAttrRepository.UpdateAsync(itemGroupAttr);
        }

    }
}