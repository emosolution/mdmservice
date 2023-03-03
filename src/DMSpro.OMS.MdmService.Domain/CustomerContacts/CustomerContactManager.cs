using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactManager : DomainService
    {
        private readonly ICustomerContactRepository _customerContactRepository;

        public CustomerContactManager(ICustomerContactRepository customerContactRepository)
        {
            _customerContactRepository = customerContactRepository;
        }

        public async Task<CustomerContact> CreateAsync(
        Guid customerId, string firstName, string lastName, Gender gender, string phone, string email, string address, string identityNumber, string bankName, string bankAccName, string bankAccNumber, Title? title = null, DateTime? dateOfBirth = null)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(firstName, nameof(firstName), CustomerContactConsts.FirstNameMaxLength);
            Check.Length(lastName, nameof(lastName), CustomerContactConsts.LastNameMaxLength);
            Check.NotNull(gender, nameof(gender));
            Check.Length(phone, nameof(phone), CustomerContactConsts.PhoneMaxLength);
            Check.Length(email, nameof(email), CustomerContactConsts.EmailMaxLength);
            Check.Length(address, nameof(address), CustomerContactConsts.AddressMaxLength);
            Check.Length(identityNumber, nameof(identityNumber), CustomerContactConsts.IdentityNumberMaxLength);
            Check.Length(bankName, nameof(bankName), CustomerContactConsts.BankNameMaxLength);
            Check.Length(bankAccName, nameof(bankAccName), CustomerContactConsts.BankAccNameMaxLength);
            Check.Length(bankAccNumber, nameof(bankAccNumber), CustomerContactConsts.BankAccNumberMaxLength);

            var customerContact = new CustomerContact(
             GuidGenerator.Create(),
             customerId, firstName, lastName, gender, phone, email, address, identityNumber, bankName, bankAccName, bankAccNumber, title, dateOfBirth
             );

            return await _customerContactRepository.InsertAsync(customerContact);
        }

        public async Task<CustomerContact> UpdateAsync(
            Guid id,
            Guid customerId, string firstName, string lastName, Gender gender, string phone, string email, string address, string identityNumber, string bankName, string bankAccName, string bankAccNumber, Title? title = null, DateTime? dateOfBirth = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(firstName, nameof(firstName), CustomerContactConsts.FirstNameMaxLength);
            Check.Length(lastName, nameof(lastName), CustomerContactConsts.LastNameMaxLength);
            Check.NotNull(gender, nameof(gender));
            Check.Length(phone, nameof(phone), CustomerContactConsts.PhoneMaxLength);
            Check.Length(email, nameof(email), CustomerContactConsts.EmailMaxLength);
            Check.Length(address, nameof(address), CustomerContactConsts.AddressMaxLength);
            Check.Length(identityNumber, nameof(identityNumber), CustomerContactConsts.IdentityNumberMaxLength);
            Check.Length(bankName, nameof(bankName), CustomerContactConsts.BankNameMaxLength);
            Check.Length(bankAccName, nameof(bankAccName), CustomerContactConsts.BankAccNameMaxLength);
            Check.Length(bankAccNumber, nameof(bankAccNumber), CustomerContactConsts.BankAccNumberMaxLength);

            var customerContact = await _customerContactRepository.GetAsync(id);

            customerContact.CustomerId = customerId;
            customerContact.FirstName = firstName;
            customerContact.LastName = lastName;
            customerContact.Gender = gender;
            customerContact.Phone = phone;
            customerContact.Email = email;
            customerContact.Address = address;
            customerContact.IdentityNumber = identityNumber;
            customerContact.BankName = bankName;
            customerContact.BankAccName = bankAccName;
            customerContact.BankAccNumber = bankAccNumber;
            customerContact.Title = title;
            customerContact.DateOfBirth = dateOfBirth;

            customerContact.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerContactRepository.UpdateAsync(customerContact);
        }

    }
}