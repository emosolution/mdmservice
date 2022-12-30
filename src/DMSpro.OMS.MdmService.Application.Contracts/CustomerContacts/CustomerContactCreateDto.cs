using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactCreateDto
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Title? Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Gender Gender { get; set; } = ((Gender[])Enum.GetValues(typeof(Gender)))[0];
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccName { get; set; }
        public string BankAccNumber { get; set; }
        public Guid CustomerId { get; set; }
    }
}