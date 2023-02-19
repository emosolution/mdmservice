using System;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public class CustomerAttachmentWithDetailsDto
	{
        public string url { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CustomerDto Customer { get; set; }


        public CustomerAttachmentWithDetailsDto()
		{
		}
	}
}

