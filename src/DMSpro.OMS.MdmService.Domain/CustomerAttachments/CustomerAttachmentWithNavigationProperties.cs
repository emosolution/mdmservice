using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentWithNavigationProperties
    {
        public CustomerAttachment CustomerAttachment { get; set; }

        public Customer Customer { get; set; }
    }
}