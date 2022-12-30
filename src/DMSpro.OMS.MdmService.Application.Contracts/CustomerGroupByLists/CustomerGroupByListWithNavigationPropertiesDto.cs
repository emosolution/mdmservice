using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListWithNavigationPropertiesDto
    {
        public CustomerGroupByListDto CustomerGroupByList { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public CustomerDto Customer { get; set; }

    }
}