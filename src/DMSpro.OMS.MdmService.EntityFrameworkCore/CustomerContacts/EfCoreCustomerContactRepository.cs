using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class EfCoreCustomerContactRepository : EfCoreRepository<MdmServiceDbContext, CustomerContact, Guid>, ICustomerContactRepository
    {
        public EfCoreCustomerContactRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerContactWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerContact => new CustomerContactWithNavigationProperties
                {
                    CustomerContact = customerContact,
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerContact.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerContactWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, title, firstName, lastName, gender, dateOfBirthMin, dateOfBirthMax, phone, email, address, identityNumber, bankName, bankAccName, bankAccNumber, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerContactConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerContactWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerContact in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Customers on customerContact.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()

                   select new CustomerContactWithNavigationProperties
                   {
                       CustomerContact = customerContact,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerContactWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerContactWithNavigationProperties> query,
            string filterText,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerContact.FirstName.Contains(filterText) || e.CustomerContact.LastName.Contains(filterText) || e.CustomerContact.Phone.Contains(filterText) || e.CustomerContact.Email.Contains(filterText) || e.CustomerContact.Address.Contains(filterText) || e.CustomerContact.IdentityNumber.Contains(filterText) || e.CustomerContact.BankName.Contains(filterText) || e.CustomerContact.BankAccName.Contains(filterText) || e.CustomerContact.BankAccNumber.Contains(filterText))
                    .WhereIf(title.HasValue, e => e.CustomerContact.Title == title)
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.CustomerContact.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.CustomerContact.LastName.Contains(lastName))
                    .WhereIf(gender.HasValue, e => e.CustomerContact.Gender == gender)
                    .WhereIf(dateOfBirthMin.HasValue, e => e.CustomerContact.DateOfBirth >= dateOfBirthMin.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.CustomerContact.DateOfBirth <= dateOfBirthMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.CustomerContact.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.CustomerContact.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.CustomerContact.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.CustomerContact.IdentityNumber.Contains(identityNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankName), e => e.CustomerContact.BankName.Contains(bankName))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankAccName), e => e.CustomerContact.BankAccName.Contains(bankAccName))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankAccNumber), e => e.CustomerContact.BankAccNumber.Contains(bankAccNumber))
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerContact>> GetListAsync(
            string filterText = null,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, title, firstName, lastName, gender, dateOfBirthMin, dateOfBirthMax, phone, email, address, identityNumber, bankName, bankAccName, bankAccNumber);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerContactConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, title, firstName, lastName, gender, dateOfBirthMin, dateOfBirthMax, phone, email, address, identityNumber, bankName, bankAccName, bankAccNumber, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerContact> ApplyFilter(
            IQueryable<CustomerContact> query,
            string filterText,
            Title? title = null,
            string firstName = null,
            string lastName = null,
            Gender? gender = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string phone = null,
            string email = null,
            string address = null,
            string identityNumber = null,
            string bankName = null,
            string bankAccName = null,
            string bankAccNumber = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName.Contains(filterText) || e.LastName.Contains(filterText) || e.Phone.Contains(filterText) || e.Email.Contains(filterText) || e.Address.Contains(filterText) || e.IdentityNumber.Contains(filterText) || e.BankName.Contains(filterText) || e.BankAccName.Contains(filterText) || e.BankAccNumber.Contains(filterText))
                    .WhereIf(title.HasValue, e => e.Title == title)
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(gender.HasValue, e => e.Gender == gender)
                    .WhereIf(dateOfBirthMin.HasValue, e => e.DateOfBirth >= dateOfBirthMin.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.DateOfBirth <= dateOfBirthMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(identityNumber), e => e.IdentityNumber.Contains(identityNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankName), e => e.BankName.Contains(bankName))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankAccName), e => e.BankAccName.Contains(bankAccName))
                    .WhereIf(!string.IsNullOrWhiteSpace(bankAccNumber), e => e.BankAccNumber.Contains(bankAccNumber));
        }
    }
}