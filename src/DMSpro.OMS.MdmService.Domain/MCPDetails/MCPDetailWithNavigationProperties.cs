using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.MCPHeaders;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailWithNavigationProperties
    {
        public MCPDetail MCPDetail { get; set; }

        public Customer Customer { get; set; }
        public MCPHeader MCPHeader { get; set; }
        

        
    }
}