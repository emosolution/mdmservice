using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public partial class CustomerContact : FullAuditedAggregateRoot<Guid>, IMultiTenant
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

        public virtual Customer Customer { get; set; }

        public CustomerContact()
        {

        }

        public CustomerContact(Guid id, Guid customerId, string firstName, string lastName, Gender gender, string phone, string email, string address, string identityNumber, string bankName, string bankAccName, string bankAccNumber, Title? title = null, DateTime? dateOfBirth = null)
        {

            Id = id;
            Check.Length(firstName, nameof(firstName), CustomerContactConsts.FirstNameMaxLength, 0);
            Check.Length(lastName, nameof(lastName), CustomerContactConsts.LastNameMaxLength, 0);
            Check.Length(phone, nameof(phone), CustomerContactConsts.PhoneMaxLength, 0);
            Check.Length(email, nameof(email), CustomerContactConsts.EmailMaxLength, 0);
            Check.Length(address, nameof(address), CustomerContactConsts.AddressMaxLength, 0);
            Check.Length(identityNumber, nameof(identityNumber), CustomerContactConsts.IdentityNumberMaxLength, 0);
            Check.Length(bankName, nameof(bankName), CustomerContactConsts.BankNameMaxLength, 0);
            Check.Length(bankAccName, nameof(bankAccName), CustomerContactConsts.BankAccNameMaxLength, 0);
            Check.Length(bankAccNumber, nameof(bankAccNumber), CustomerContactConsts.BankAccNumberMaxLength, 0);
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