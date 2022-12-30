using DMSpro.OMS.MdmService.Customers;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactWithNavigationPropertiesDto
    {
        public CustomerContactDto CustomerContact { get; set; }

        public CustomerDto Customer { get; set; }

    }
}