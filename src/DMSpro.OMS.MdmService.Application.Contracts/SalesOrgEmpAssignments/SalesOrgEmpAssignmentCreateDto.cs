using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentCreateDto
    {
        public bool IsBase { get; set; } = false;
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(SalesOrgEmpAssignmentConsts.HierarchyCodeMaxLength)]
        public string HierarchyCode { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeProfileId { get; set; }
    }
}