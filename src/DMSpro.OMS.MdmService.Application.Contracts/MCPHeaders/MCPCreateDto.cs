using DMSpro.OMS.MdmService.MCPDetails;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPCreateDto
    {
        public MCPHeaderCreateDto MCPHeaderDto { get; set; }
        public List<MCPDetailCreateDto> MCPDetails { get; set; } 
    }
}
