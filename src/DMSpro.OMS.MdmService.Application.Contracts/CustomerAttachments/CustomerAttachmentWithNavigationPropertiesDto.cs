using DMSpro.OMS.MdmService.Customers;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentWithNavigationPropertiesDto
    {
        public CustomerAttachmentDto CustomerAttachment { get; set; }

        public CustomerDto Customer { get; set; }

    }
}