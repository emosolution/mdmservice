using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CusAttributeValues;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttWithNavigationPropertiesDto
    {
        public CustomerGroupByAttDto CustomerGroupByAtt { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public CusAttributeValueDto CusAttributeValue { get; set; }

    }
}