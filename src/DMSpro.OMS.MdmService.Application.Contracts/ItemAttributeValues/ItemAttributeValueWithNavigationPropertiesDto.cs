using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.ItemAttributeValues;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueWithNavigationPropertiesDto
    {
        public ItemAttributeValueDto ItemAttributeValue { get; set; }

        public ItemAttributeDto ItemAttribute { get; set; }
        public ItemAttributeValueDto ItemAttributeValue1 { get; set; }

    }
}