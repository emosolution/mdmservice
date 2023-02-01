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

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public partial class EfCoreCustomerAttachmentRepository : EfCoreRepository<MdmServiceDbContext, CustomerAttachment, Guid>, ICustomerAttachmentRepository
    {
        public EfCoreCustomerAttachmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerAttachment => new CustomerAttachmentWithNavigationProperties
                {
                    CustomerAttachment = customerAttachment,
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerAttachment.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string url = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, url, description, active, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAttachmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerAttachmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerAttachment in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Customers on customerAttachment.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()

                   select new CustomerAttachmentWithNavigationProperties
                   {
                       CustomerAttachment = customerAttachment,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerAttachmentWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerAttachmentWithNavigationProperties> query,
            string filterText,
            string url = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerAttachment.url.Contains(filterText) || e.CustomerAttachment.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.CustomerAttachment.url.Contains(url))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CustomerAttachment.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.CustomerAttachment.Active == active)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerAttachment>> GetListAsync(
            string filterText = null,
            string url = null,
            string description = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, url, description, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAttachmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string url = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, url, description, active, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerAttachment> ApplyFilter(
            IQueryable<CustomerAttachment> query,
            string filterText,
            string url = null,
            string description = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.url.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.url.Contains(url))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}