using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactWithNavigationProperties
    {
        public CustomerContact CustomerContact { get; set; }

        public Customer Customer { get; set; }
    }
}