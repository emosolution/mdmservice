using System;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public class CustomerAttachmentWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CustomerDto Customer { get; set; }


        public CustomerAttachmentWithDetailsDto()
		{
		}
	}
}

