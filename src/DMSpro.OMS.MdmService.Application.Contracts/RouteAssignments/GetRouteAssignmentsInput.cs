using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class GetRouteAssignmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public Guid? RouteId { get; set; }
        public Guid? EmployeeId { get; set; }

        public GetRouteAssignmentsInput()
        {

        }
    }
}