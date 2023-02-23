using DMSpro.OMS.MdmService.CustomerAttachments;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerProfileDto
    {
        public CustomerDto Customer { get; set; }
        public List<CustomerAttachmentDto> Attachments { get; set; }    
    }
}
