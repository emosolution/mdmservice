using System;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
	public class CustomerContactWithDetailsDto
	{
        
        public Title? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccName { get; set; }
        public string BankAccNumber { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerDto Customer { get; set; }

        public CustomerContactWithDetailsDto()
		{
		}
	}
}

