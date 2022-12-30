using DMSpro.OMS.MdmService.CustomerAttributes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueWithNavigationPropertiesDto
    {
        public CusAttributeValueDto CusAttributeValue { get; set; }

        public CustomerAttributeDto CustomerAttribute { get; set; }
        public CusAttributeValueDto CusAttributeValue1 { get; set; }

    }
}