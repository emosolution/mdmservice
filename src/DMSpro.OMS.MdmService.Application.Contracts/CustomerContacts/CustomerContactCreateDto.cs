using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactCreateDto
    {
        public Title? Title { get; set; }
        [StringLength(CustomerContactConsts.FirstNameMaxLength)]
        public string FirstName { get; set; }
        [StringLength(CustomerContactConsts.LastNameMaxLength)]
        public string LastName { get; set; }
        public Gender Gender { get; set; } = ((Gender[])Enum.GetValues(typeof(Gender)))[0];
        public DateTime? DateOfBirth { get; set; }
        [StringLength(CustomerContactConsts.PhoneMaxLength)]
        public string Phone { get; set; }
        [EmailAddress]
        [StringLength(CustomerContactConsts.EmailMaxLength)]
        public string Email { get; set; }
        [StringLength(CustomerContactConsts.AddressMaxLength)]
        public string Address { get; set; }
        [StringLength(CustomerContactConsts.IdentityNumberMaxLength)]
        public string IdentityNumber { get; set; }
        [StringLength(CustomerContactConsts.BankNameMaxLength)]
        public string BankName { get; set; }
        [StringLength(CustomerContactConsts.BankAccNameMaxLength)]
        public string BankAccName { get; set; }
        [StringLength(CustomerContactConsts.BankAccNumberMaxLength)]
        public string BankAccNumber { get; set; }
        public Guid CustomerId { get; set; }
    }
}