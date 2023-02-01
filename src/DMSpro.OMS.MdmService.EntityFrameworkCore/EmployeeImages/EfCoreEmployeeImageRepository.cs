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

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial class EfCoreEmployeeImageRepository : EfCoreRepository<MdmServiceDbContext, EmployeeImage, Guid>, IEmployeeImageRepository
    {
        public EfCoreEmployeeImageRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<EmployeeImageWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employeeImage => new EmployeeImageWithNavigationProperties
                {
                    EmployeeImage = employeeImage,
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == employeeImage.EmployeeProfileId)
                }).FirstOrDefault();
        }

        public async Task<List<EmployeeImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null,
            Guid? employeeProfileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, url, active, isAvatar, employeeProfileId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeImageConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeImageWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employeeImage in (await GetDbSetAsync())
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on employeeImage.EmployeeProfileId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new EmployeeImageWithNavigationProperties
                   {
                       EmployeeImage = employeeImage,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<EmployeeImageWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeImageWithNavigationProperties> query,
            string filterText,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null,
            Guid? employeeProfileId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EmployeeImage.Description.Contains(filterText) || e.EmployeeImage.url.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.EmployeeImage.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.EmployeeImage.url.Contains(url))
                    .WhereIf(active.HasValue, e => e.EmployeeImage.Active == active)
                    .WhereIf(isAvatar.HasValue, e => e.EmployeeImage.IsAvatar == isAvatar)
                    .WhereIf(employeeProfileId != null && employeeProfileId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeProfileId);
        }

        public async Task<List<EmployeeImage>> GetListAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, url, active, isAvatar);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeImageConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null,
            Guid? employeeProfileId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, url, active, isAvatar, employeeProfileId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeImage> ApplyFilter(
            IQueryable<EmployeeImage> query,
            string filterText,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText) || e.url.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.url.Contains(url))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(isAvatar.HasValue, e => e.IsAvatar == isAvatar);
        }
    }
}