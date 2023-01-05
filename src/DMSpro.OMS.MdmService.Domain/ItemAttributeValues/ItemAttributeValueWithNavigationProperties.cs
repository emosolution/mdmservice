using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.ItemAttributeValues;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueWithNavigationProperties
    {
        public ItemAttributeValue ItemAttributeValue { get; set; }

        public ItemAttribute ItemAttribute { get; set; }
        public ItemAttributeValue ItemAttributeValue1 { get; set; }
        

        
    }
}