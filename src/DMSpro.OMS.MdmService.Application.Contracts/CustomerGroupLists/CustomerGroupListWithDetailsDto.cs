using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListWithDetailsDto : CustomerGroupListDto, IHasConcurrencyStamp
    {
        public CustomerDto Customer { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }

        public CustomerGroupListWithDetailsDto()
        {

        }
    }
}