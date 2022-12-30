using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class GetCustomerGroupByAttsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string ValueCode { get; set; }
        public string ValueName { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? CusAttributeValueId { get; set; }

        public GetCustomerGroupByAttsInput()
        {

        }
    }
}