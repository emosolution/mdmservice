using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZoneWithNavigationPropertiesDto
    {
        public CustomerInZoneDto CustomerInZone { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public CustomerDto Customer { get; set; }

    }
}