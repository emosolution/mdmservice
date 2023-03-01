using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial class EfCoreEmployeeAttachmentRepository : EfCoreRepository<MdmServiceDbContext, EmployeeAttachment, Guid>, IEmployeeAttachmentRepository
    {
        public EfCoreEmployeeAttachmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<EmployeeAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employeeAttachment => new EmployeeAttachmentWithNavigationProperties
                {
                    EmployeeAttachment = employeeAttachment,
                    EmployeeProfile = dbContext.EmployeeProfiles.FirstOrDefault(c => c.Id == employeeAttachment.EmployeeProfileId)
                }).FirstOrDefault();
        }

        public async Task<List<EmployeeAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? fileId = null,
            Guid? employeeProfileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, fileId, employeeProfileId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAttachmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeAttachmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employeeAttachment in (await GetDbSetAsync())
                   join employeeProfile in (await GetDbContextAsync()).EmployeeProfiles on employeeAttachment.EmployeeProfileId equals employeeProfile.Id into employeeProfiles
                   from employeeProfile in employeeProfiles.DefaultIfEmpty()

                   select new EmployeeAttachmentWithNavigationProperties
                   {
                       EmployeeAttachment = employeeAttachment,
                       EmployeeProfile = employeeProfile
                   };
        }

        protected virtual IQueryable<EmployeeAttachmentWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeAttachmentWithNavigationProperties> query,
            string filterText,
            string description = null,
            bool? active = null,
            Guid? fileId = null,
            Guid? employeeProfileId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EmployeeAttachment.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.EmployeeAttachment.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.EmployeeAttachment.Active == active)
                    .WhereIf(fileId.HasValue, e => e.EmployeeAttachment.FileId == fileId)
                    .WhereIf(employeeProfileId != null && employeeProfileId != Guid.Empty, e => e.EmployeeProfile != null && e.EmployeeProfile.Id == employeeProfileId);
        }

        public async Task<List<EmployeeAttachment>> GetListAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? fileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, active, fileId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeAttachmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? fileId = null,
            Guid? employeeProfileId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, fileId, employeeProfileId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeAttachment> ApplyFilter(
            IQueryable<EmployeeAttachment> query,
            string filterText,
            string description = null,
            bool? active = null,
            Guid? fileId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(fileId.HasValue, e => e.FileId == fileId);
        }
    }
}