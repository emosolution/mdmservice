using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueWithNavigationPropertiesDto
    {
        public CustomerAttributeValueDto CustomerAttributeValue { get; set; }

        public CustomerAttributeDto CustomerAttribute { get; set; }
        public CustomerAttributeValueDto CustomerAttributeValue1 { get; set; }

    }
}