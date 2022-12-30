using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class GetCustomerContactsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public Title? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirthMin { get; set; }
        public DateTime? DateOfBirthMax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccName { get; set; }
        public string BankAccNumber { get; set; }
        public Guid? CustomerId { get; set; }

        public GetCustomerContactsInput()
        {

        }
    }
}