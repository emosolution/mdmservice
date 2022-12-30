using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Routes
{
    public class GetRoutesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? CheckIn { get; set; }
        public bool? CheckOut { get; set; }
        public bool? GPSLock { get; set; }
        public bool? OutRoute { get; set; }
        public Guid? RouteTypeId { get; set; }
        public Guid? ItemGroupId { get; set; }
        public Guid? SalesOrgHierarchyId { get; set; }

        public GetRoutesInput()
        {

        }
    }
}