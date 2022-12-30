using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class GetEmployeeInZonesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public Guid? EndDate { get; set; }
        public Guid? SalesOrgHierarchyId { get; set; }
        public Guid? EmployeeId { get; set; }

        public GetEmployeeInZonesInput()
        {

        }
    }
}