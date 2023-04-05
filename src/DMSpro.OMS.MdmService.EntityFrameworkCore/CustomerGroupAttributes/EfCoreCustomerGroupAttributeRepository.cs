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

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class EfCoreCustomerGroupAttributeRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupAttribute, Guid>, ICustomerGroupAttributeRepository
    {
        public EfCoreCustomerGroupAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupAttributeWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupAttribute => new CustomerGroupAttributeWithNavigationProperties
                {
                    CustomerGroupAttribute = customerGroupAttribute,
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupAttribute.CustomerGroupId),
                    CustomerAttributeValue = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr0Id),
                    CustomerAttributeValue1 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr1Id),
                    CustomerAttributeValue2 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr2Id),
                    CustomerAttributeValue3 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr3Id),
                    CustomerAttributeValue4 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr4Id),
                    CustomerAttributeValue5 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr5Id),
                    CustomerAttributeValue6 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr6Id),
                    CustomerAttributeValue7 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr7Id),
                    CustomerAttributeValue8 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr8Id),
                    CustomerAttributeValue9 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr9Id),
                    CustomerAttributeValue10 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr10Id),
                    CustomerAttributeValue11 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr11Id),
                    CustomerAttributeValue12 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr12Id),
                    CustomerAttributeValue13 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr13Id),
                    CustomerAttributeValue14 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr14Id),
                    CustomerAttributeValue15 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr15Id),
                    CustomerAttributeValue16 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr16Id),
                    CustomerAttributeValue17 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr17Id),
                    CustomerAttributeValue18 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr18Id),
                    CustomerAttributeValue19 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerGroupAttribute.Attr19Id)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupAttributeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            Guid? customerGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, customerGroupId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupAttributeConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupAttributeWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupAttribute in (await GetDbSetAsync())
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupAttribute.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()
                   join customerAttributeValue in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr0Id equals customerAttributeValue.Id into customerAttributeValues
                   from customerAttributeValue in customerAttributeValues.DefaultIfEmpty()
                   join customerAttributeValue1 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr1Id equals customerAttributeValue1.Id into customerAttributeValues1
                   from customerAttributeValue1 in customerAttributeValues1.DefaultIfEmpty()
                   join customerAttributeValue2 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr2Id equals customerAttributeValue2.Id into customerAttributeValues2
                   from customerAttributeValue2 in customerAttributeValues2.DefaultIfEmpty()
                   join customerAttributeValue3 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr3Id equals customerAttributeValue3.Id into customerAttributeValues3
                   from customerAttributeValue3 in customerAttributeValues3.DefaultIfEmpty()
                   join customerAttributeValue4 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr4Id equals customerAttributeValue4.Id into customerAttributeValues4
                   from customerAttributeValue4 in customerAttributeValues4.DefaultIfEmpty()
                   join customerAttributeValue5 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr5Id equals customerAttributeValue5.Id into customerAttributeValues5
                   from customerAttributeValue5 in customerAttributeValues5.DefaultIfEmpty()
                   join customerAttributeValue6 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr6Id equals customerAttributeValue6.Id into customerAttributeValues6
                   from customerAttributeValue6 in customerAttributeValues6.DefaultIfEmpty()
                   join customerAttributeValue7 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr7Id equals customerAttributeValue7.Id into customerAttributeValues7
                   from customerAttributeValue7 in customerAttributeValues7.DefaultIfEmpty()
                   join customerAttributeValue8 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr8Id equals customerAttributeValue8.Id into customerAttributeValues8
                   from customerAttributeValue8 in customerAttributeValues8.DefaultIfEmpty()
                   join customerAttributeValue9 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr9Id equals customerAttributeValue9.Id into customerAttributeValues9
                   from customerAttributeValue9 in customerAttributeValues9.DefaultIfEmpty()
                   join customerAttributeValue10 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr10Id equals customerAttributeValue10.Id into customerAttributeValues10
                   from customerAttributeValue10 in customerAttributeValues10.DefaultIfEmpty()
                   join customerAttributeValue11 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr11Id equals customerAttributeValue11.Id into customerAttributeValues11
                   from customerAttributeValue11 in customerAttributeValues11.DefaultIfEmpty()
                   join customerAttributeValue12 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr12Id equals customerAttributeValue12.Id into customerAttributeValues12
                   from customerAttributeValue12 in customerAttributeValues12.DefaultIfEmpty()
                   join customerAttributeValue13 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr13Id equals customerAttributeValue13.Id into customerAttributeValues13
                   from customerAttributeValue13 in customerAttributeValues13.DefaultIfEmpty()
                   join customerAttributeValue14 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr14Id equals customerAttributeValue14.Id into customerAttributeValues14
                   from customerAttributeValue14 in customerAttributeValues14.DefaultIfEmpty()
                   join customerAttributeValue15 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr15Id equals customerAttributeValue15.Id into customerAttributeValues15
                   from customerAttributeValue15 in customerAttributeValues15.DefaultIfEmpty()
                   join customerAttributeValue16 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr16Id equals customerAttributeValue16.Id into customerAttributeValues16
                   from customerAttributeValue16 in customerAttributeValues16.DefaultIfEmpty()
                   join customerAttributeValue17 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr17Id equals customerAttributeValue17.Id into customerAttributeValues17
                   from customerAttributeValue17 in customerAttributeValues17.DefaultIfEmpty()
                   join customerAttributeValue18 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr18Id equals customerAttributeValue18.Id into customerAttributeValues18
                   from customerAttributeValue18 in customerAttributeValues18.DefaultIfEmpty()
                   join customerAttributeValue19 in (await GetDbContextAsync()).CustomerAttributeValues on customerGroupAttribute.Attr19Id equals customerAttributeValue19.Id into customerAttributeValues19
                   from customerAttributeValue19 in customerAttributeValues19.DefaultIfEmpty()

                   select new CustomerGroupAttributeWithNavigationProperties
                   {
                       CustomerGroupAttribute = customerGroupAttribute,
                       CustomerGroup = customerGroup,
                       CustomerAttributeValue = customerAttributeValue,
                       CustomerAttributeValue1 = customerAttributeValue1,
                       CustomerAttributeValue2 = customerAttributeValue2,
                       CustomerAttributeValue3 = customerAttributeValue3,
                       CustomerAttributeValue4 = customerAttributeValue4,
                       CustomerAttributeValue5 = customerAttributeValue5,
                       CustomerAttributeValue6 = customerAttributeValue6,
                       CustomerAttributeValue7 = customerAttributeValue7,
                       CustomerAttributeValue8 = customerAttributeValue8,
                       CustomerAttributeValue9 = customerAttributeValue9,
                       CustomerAttributeValue10 = customerAttributeValue10,
                       CustomerAttributeValue11 = customerAttributeValue11,
                       CustomerAttributeValue12 = customerAttributeValue12,
                       CustomerAttributeValue13 = customerAttributeValue13,
                       CustomerAttributeValue14 = customerAttributeValue14,
                       CustomerAttributeValue15 = customerAttributeValue15,
                       CustomerAttributeValue16 = customerAttributeValue16,
                       CustomerAttributeValue17 = customerAttributeValue17,
                       CustomerAttributeValue18 = customerAttributeValue18,
                       CustomerAttributeValue19 = customerAttributeValue19
                   };
        }

        protected virtual IQueryable<CustomerGroupAttributeWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupAttributeWithNavigationProperties> query,
            string filterText,
            string description = null,
            Guid? customerGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerGroupAttribute.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CustomerGroupAttribute.Description.Contains(description))
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId)
                    .WhereIf(attr0Id != null && attr0Id != Guid.Empty, e => e.CustomerAttributeValue != null && e.CustomerAttributeValue.Id == attr0Id)
                    .WhereIf(attr1Id != null && attr1Id != Guid.Empty, e => e.CustomerAttributeValue1 != null && e.CustomerAttributeValue1.Id == attr1Id)
                    .WhereIf(attr2Id != null && attr2Id != Guid.Empty, e => e.CustomerAttributeValue2 != null && e.CustomerAttributeValue2.Id == attr2Id)
                    .WhereIf(attr3Id != null && attr3Id != Guid.Empty, e => e.CustomerAttributeValue3 != null && e.CustomerAttributeValue3.Id == attr3Id)
                    .WhereIf(attr4Id != null && attr4Id != Guid.Empty, e => e.CustomerAttributeValue4 != null && e.CustomerAttributeValue4.Id == attr4Id)
                    .WhereIf(attr5Id != null && attr5Id != Guid.Empty, e => e.CustomerAttributeValue5 != null && e.CustomerAttributeValue5.Id == attr5Id)
                    .WhereIf(attr6Id != null && attr6Id != Guid.Empty, e => e.CustomerAttributeValue6 != null && e.CustomerAttributeValue6.Id == attr6Id)
                    .WhereIf(attr7Id != null && attr7Id != Guid.Empty, e => e.CustomerAttributeValue7 != null && e.CustomerAttributeValue7.Id == attr7Id)
                    .WhereIf(attr8Id != null && attr8Id != Guid.Empty, e => e.CustomerAttributeValue8 != null && e.CustomerAttributeValue8.Id == attr8Id)
                    .WhereIf(attr9Id != null && attr9Id != Guid.Empty, e => e.CustomerAttributeValue9 != null && e.CustomerAttributeValue9.Id == attr9Id)
                    .WhereIf(attr10Id != null && attr10Id != Guid.Empty, e => e.CustomerAttributeValue10 != null && e.CustomerAttributeValue10.Id == attr10Id)
                    .WhereIf(attr11Id != null && attr11Id != Guid.Empty, e => e.CustomerAttributeValue11 != null && e.CustomerAttributeValue11.Id == attr11Id)
                    .WhereIf(attr12Id != null && attr12Id != Guid.Empty, e => e.CustomerAttributeValue12 != null && e.CustomerAttributeValue12.Id == attr12Id)
                    .WhereIf(attr13Id != null && attr13Id != Guid.Empty, e => e.CustomerAttributeValue13 != null && e.CustomerAttributeValue13.Id == attr13Id)
                    .WhereIf(attr14Id != null && attr14Id != Guid.Empty, e => e.CustomerAttributeValue14 != null && e.CustomerAttributeValue14.Id == attr14Id)
                    .WhereIf(attr15Id != null && attr15Id != Guid.Empty, e => e.CustomerAttributeValue15 != null && e.CustomerAttributeValue15.Id == attr15Id)
                    .WhereIf(attr16Id != null && attr16Id != Guid.Empty, e => e.CustomerAttributeValue16 != null && e.CustomerAttributeValue16.Id == attr16Id)
                    .WhereIf(attr17Id != null && attr17Id != Guid.Empty, e => e.CustomerAttributeValue17 != null && e.CustomerAttributeValue17.Id == attr17Id)
                    .WhereIf(attr18Id != null && attr18Id != Guid.Empty, e => e.CustomerAttributeValue18 != null && e.CustomerAttributeValue18.Id == attr18Id)
                    .WhereIf(attr19Id != null && attr19Id != Guid.Empty, e => e.CustomerAttributeValue19 != null && e.CustomerAttributeValue19.Id == attr19Id);
        }

        public async Task<List<CustomerGroupAttribute>> GetListAsync(
            string filterText = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            Guid? customerGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr5Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, customerGroupId, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupAttribute> ApplyFilter(
            IQueryable<CustomerGroupAttribute> query,
            string filterText,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}