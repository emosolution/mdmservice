using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial class EfCoreSalesOrgHierarchyRepository
    {
        public virtual async Task<List<SalesOrgHierarchy>> GetChildrenAsync(
            Guid? parentId,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                //.IncludeDetails(includeDetails)
                .Where(x => x.ParentId == parentId)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<SalesOrgHierarchy>> GetAllChildrenWithParentCodeAsync(
        string code,
        Guid? parentId,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                //.IncludeDetails(includeDetails)
                .Where(ou => ou.HierarchyCode.StartsWith(code) && ou.Id != parentId.Value)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<SalesOrgHierarchy>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                //.IncludeDetails(includeDetails)
                .OrderBy(sorting.IsNullOrEmpty() ? nameof(SalesOrgHierarchy.Name) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<SalesOrgHierarchy>> GetListAsync(
            IEnumerable<Guid> ids,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                //.IncludeDetails(includeDetails)
                .Where(t => ids.Contains(t.Id))
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<SalesOrgHierarchy> GetAsync(
            string displayName,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                //.IncludeDetails(includeDetails)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(
                    ou => ou.Name == displayName,
                    GetCancellationToken(cancellationToken)
                );
        }

        public virtual async Task RemoveAllMembersAsync(
            SalesOrgHierarchy organizationUnit,
            CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            var ouMembersQuery = await dbContext.Set<SalesOrgEmpAssignment>()
                .Where(q => q.SalesOrgHierarchyId == organizationUnit.Id)
                .ToListAsync(GetCancellationToken(cancellationToken));

            dbContext.Set<SalesOrgEmpAssignment>().RemoveRange(ouMembersQuery);
        }
    }
}