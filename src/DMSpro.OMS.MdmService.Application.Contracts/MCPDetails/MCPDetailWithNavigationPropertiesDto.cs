using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.MCPHeaders;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailWithNavigationPropertiesDto
    {
        public MCPDetailDto MCPDetail { get; set; }

        public CustomerDto Customer { get; set; }
        public MCPHeaderDto MCPHeader { get; set; }

    }
}