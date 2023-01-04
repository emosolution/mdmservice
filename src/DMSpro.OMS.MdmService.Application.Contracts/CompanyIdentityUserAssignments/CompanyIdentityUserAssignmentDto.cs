using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid IdentityUserId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}