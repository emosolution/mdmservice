using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class GetCustomerGroupByListsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? Active { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? CustomerId { get; set; }

        public GetCustomerGroupByListsInput()
        {

        }
    }
}