using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class GetCustomerAttributesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? AttrNoMin { get; set; }
        public int? AttrNoMax { get; set; }
        public string AttrName { get; set; }
        public int? HierarchyLevelMin { get; set; }
        public int? HierarchyLevelMax { get; set; }
        public bool? Active { get; set; }

        public GetCustomerAttributesInput()
        {

        }
    }
}