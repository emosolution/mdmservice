using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class GetCustomerAttributeValuesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string AttrValName { get; set; }
        public Guid? CustomerAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public GetCustomerAttributeValuesInput()
        {

        }
    }
}