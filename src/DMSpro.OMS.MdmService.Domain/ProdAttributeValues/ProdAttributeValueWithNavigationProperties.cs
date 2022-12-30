using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.ProdAttributeValues;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueWithNavigationProperties
    {
        public ProdAttributeValue ProdAttributeValue { get; set; }

        public ProductAttribute ProductAttribute { get; set; }
        public ProdAttributeValue ProdAttributeValue1 { get; set; }
        

        
    }
}