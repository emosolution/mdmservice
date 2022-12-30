using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid RouteId { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}