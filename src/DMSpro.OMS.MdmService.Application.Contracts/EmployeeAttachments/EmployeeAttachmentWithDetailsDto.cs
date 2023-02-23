using System;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	public class EmployeeAttachmentWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

        public EmployeeAttachmentWithDetailsDto()
		{
		}
	}
}

