using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class GetItemAttributeValuesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string AttrValName { get; set; }
        public Guid? ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public GetItemAttributeValuesInput()
        {

        }
    }
}