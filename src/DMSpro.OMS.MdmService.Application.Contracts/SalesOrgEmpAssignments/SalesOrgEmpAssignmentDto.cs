using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool IsBase { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}