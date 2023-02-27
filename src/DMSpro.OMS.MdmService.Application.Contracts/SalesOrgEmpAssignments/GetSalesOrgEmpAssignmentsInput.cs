using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class GetSalesOrgEmpAssignmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? IsBase { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public string HierarchyCode { get; set; }
        public Guid? SalesOrgHierarchyId { get; set; }
        public Guid? EmployeeProfileId { get; set; }

        public GetSalesOrgEmpAssignmentsInput()
        {

        }
    }
}