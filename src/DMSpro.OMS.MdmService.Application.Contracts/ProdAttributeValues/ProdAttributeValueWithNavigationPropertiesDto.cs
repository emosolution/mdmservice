using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.ProdAttributeValues;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueWithNavigationPropertiesDto
    {
        public ProdAttributeValueDto ProdAttributeValue { get; set; }

        public ProductAttributeDto ProductAttribute { get; set; }
        public ProdAttributeValueDto ProdAttributeValue1 { get; set; }

    }
}