using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class GetItemAttributesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? AttrNoMin { get; set; }
        public int? AttrNoMax { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevelMin { get; set; }
        public int? HierarchyLevelMax { get; set; }
        public bool? Active { get; set; }
        public bool? IsSellingCategory { get; set; }

        public GetItemAttributesInput()
        {

        }
    }
}