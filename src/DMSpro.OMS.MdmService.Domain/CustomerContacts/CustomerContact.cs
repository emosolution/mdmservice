using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContact : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Title? Title { get; set; }

        [CanBeNull]
        public virtual string FirstName { get; set; }

        [CanBeNull]
        public virtual string LastName { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        [CanBeNull]
        public virtual string Phone { get; set; }

        [CanBeNull]
        public virtual string Email { get; set; }

        [CanBeNull]
        public virtual string Address { get; set; }

        [CanBeNull]
        public virtual string IdentityNumber { get; set; }

        [CanBeNull]
        public virtual string BankName { get; set; }

        [CanBeNull]
        public virtual string BankAccName { get; set; }

        [CanBeNull]
        public virtual string BankAccNumber { get; set; }
        public Guid CustomerId { get; set; }

        public CustomerContact()
        {

        }

        public CustomerContact(Guid id, Guid customerId, string firstName, string lastName, Gender gender, string phone, string email, string address, string identityNumber, string bankName, string bankAccName, string bankAccNumber, Title? title = null, DateTime? dateOfBirth = null)
        {

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Phone = phone;
            Email = email;
            Address = address;
            IdentityNumber = identityNumber;
            BankName = bankName;
            BankAccName = bankAccName;
            BankAccNumber = bankAccNumber;
            Title = title;
            DateOfBirth = dateOfBirth;
            CustomerId = customerId;
        }

    }
}