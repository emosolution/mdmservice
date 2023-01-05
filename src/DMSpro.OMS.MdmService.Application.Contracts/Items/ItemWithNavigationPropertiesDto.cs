using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.ItemAttributeValues;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemWithNavigationPropertiesDto
    {
        public ItemDto Item { get; set; }

        public SystemDataDto SystemData { get; set; }
        public VATDto VAT { get; set; }
        public UOMGroupDto UOMGroup { get; set; }
        public UOMGroupDetailDto UOMGroupDetail { get; set; }
        public UOMGroupDetailDto UOMGroupDetail1 { get; set; }
        public UOMGroupDetailDto UOMGroupDetail2 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue { get; set; }
        public ItemAttributeValueDto ItemAttributeValue1 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue2 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue3 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue4 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue5 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue6 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue7 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue8 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue9 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue10 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue11 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue12 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue13 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue14 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue15 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue16 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue17 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue18 { get; set; }
        public ItemAttributeValueDto ItemAttributeValue19 { get; set; }

    }
}