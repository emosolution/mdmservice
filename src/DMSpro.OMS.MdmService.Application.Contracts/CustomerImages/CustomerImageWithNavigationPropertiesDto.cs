using DMSpro.OMS.MdmService.Customers;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageWithNavigationPropertiesDto
    {
        public CustomerImageDto CustomerImage { get; set; }

        public CustomerDto Customer { get; set; }

    }
}