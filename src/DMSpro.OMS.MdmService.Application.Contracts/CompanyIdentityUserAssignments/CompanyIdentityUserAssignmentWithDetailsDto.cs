using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using DMSpro.OMS.MdmService.Companies;
namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid IdentityUserId { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyDto Company { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}