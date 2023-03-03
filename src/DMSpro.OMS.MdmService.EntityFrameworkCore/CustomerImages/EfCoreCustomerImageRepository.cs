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

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class EfCoreCustomerImageRepository : EfCoreRepository<MdmServiceDbContext, CustomerImage, Guid>, ICustomerImageRepository
    {
        public EfCoreCustomerImageRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerImageWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerImage => new CustomerImageWithNavigationProperties
                {
                    CustomerImage = customerImage,
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerImage.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, isAvatar, isPOSM, fileId, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerImageConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerImageWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerImage in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Customers on customerImage.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()

                   select new CustomerImageWithNavigationProperties
                   {
                       CustomerImage = customerImage,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerImageWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerImageWithNavigationProperties> query,
            string filterText,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerImage.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CustomerImage.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.CustomerImage.Active == active)
                    .WhereIf(isAvatar.HasValue, e => e.CustomerImage.IsAvatar == isAvatar)
                    .WhereIf(isPOSM.HasValue, e => e.CustomerImage.IsPOSM == isPOSM)
                    .WhereIf(fileId.HasValue, e => e.CustomerImage.FileId == fileId)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerImage>> GetListAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, active, isAvatar, isPOSM, fileId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerImageConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, isAvatar, isPOSM, fileId, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerImage> ApplyFilter(
            IQueryable<CustomerImage> query,
            string filterText,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(isAvatar.HasValue, e => e.IsAvatar == isAvatar)
                    .WhereIf(isPOSM.HasValue, e => e.IsPOSM == isPOSM)
                    .WhereIf(fileId.HasValue, e => e.FileId == fileId);
        }
    }
}