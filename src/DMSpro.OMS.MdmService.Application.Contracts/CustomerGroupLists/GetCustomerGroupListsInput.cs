using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class GetCustomerGroupListsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CustomerGroupId { get; set; }

        public GetCustomerGroupListsInput()
        {

        }
    }
}