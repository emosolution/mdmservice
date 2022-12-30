using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class GetCusAttributeValuesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string AttrValName { get; set; }
        public Guid? CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }

        public GetCusAttributeValuesInput()
        {

        }
    }
}