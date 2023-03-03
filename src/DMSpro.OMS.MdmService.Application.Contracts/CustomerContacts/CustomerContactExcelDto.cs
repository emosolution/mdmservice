using System;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactExcelDto
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
    }
}