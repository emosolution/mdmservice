using DMSpro.OMS.MdmService.MCPDetails;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPDto
    {
        public MCPHeaderDto MCPHeaderDto { get; set; }  
        public List<MCPDetailDto> MCPDetails { get; set; }
    }
}
