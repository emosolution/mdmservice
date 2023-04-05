using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroups;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListWithNavigationPropertiesDto
    {
        public CustomerGroupListDto CustomerGroupList { get; set; }

        public CustomerDto Customer { get; set; }
        public CustomerGroupDto CustomerGroup { get; set; }

    }
}