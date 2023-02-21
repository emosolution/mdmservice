using DMSpro.OMS.MdmService.MCPDetails;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPUpdateDto
    {
        public MCPHeaderUpdateDto MCPHeaderDto { get; set; }
        public List<MCPDetailUpdateWithIdDto> MCPDetails { get; set; }
    }
}
