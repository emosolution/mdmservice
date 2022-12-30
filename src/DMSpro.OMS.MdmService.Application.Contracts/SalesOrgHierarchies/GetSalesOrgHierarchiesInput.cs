using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class GetSalesOrgHierarchiesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int? LevelMin { get; set; }
        public int? LevelMax { get; set; }
        public bool? IsRoute { get; set; }
        public bool? IsSellingZone { get; set; }
        public string HierarchyCode { get; set; }
        public bool? Active { get; set; }
        public Guid? SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }

        public GetSalesOrgHierarchiesInput()
        {

        }
    }
}