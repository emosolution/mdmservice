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

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial class EfCoreItemAttachmentRepository : EfCoreRepository<MdmServiceDbContext, ItemAttachment, Guid>, IItemAttachmentRepository
    {
        public EfCoreItemAttachmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ItemAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(itemAttachment => new ItemAttachmentWithNavigationProperties
                {
                    ItemAttachment = itemAttachment,
                    Item = dbContext.Items.FirstOrDefault(c => c.Id == itemAttachment.ItemId)
                }).FirstOrDefault();
        }

        public async Task<List<ItemAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, url, active, itemId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemAttachmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ItemAttachmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from itemAttachment in (await GetDbSetAsync())
                   join item in (await GetDbContextAsync()).Items on itemAttachment.ItemId equals item.Id into items
                   from item in items.DefaultIfEmpty()

                   select new ItemAttachmentWithNavigationProperties
                   {
                       ItemAttachment = itemAttachment,
                       Item = item
                   };
        }

        protected virtual IQueryable<ItemAttachmentWithNavigationProperties> ApplyFilter(
            IQueryable<ItemAttachmentWithNavigationProperties> query,
            string filterText,
            string description = null,
            string url = null,
            bool? active = null,
            Guid? itemId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ItemAttachment.Description.Contains(filterText) || e.ItemAttachment.Url.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.ItemAttachment.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.ItemAttachment.Url.Contains(url))
                    .WhereIf(active.HasValue, e => e.ItemAttachment.Active == active)
                    .WhereIf(itemId != null && itemId != Guid.Empty, e => e.Item != null && e.Item.Id == itemId);
        }

        public async Task<List<ItemAttachment>> GetListAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, url, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemAttachmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            Guid? itemId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, url, active, itemId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemAttachment> ApplyFilter(
            IQueryable<ItemAttachment> query,
            string filterText,
            string description = null,
            string url = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText) || e.Url.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.Url.Contains(url))
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}