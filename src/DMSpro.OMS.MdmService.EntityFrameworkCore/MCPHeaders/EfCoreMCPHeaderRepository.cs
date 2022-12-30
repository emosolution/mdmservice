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

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class EfCoreMCPHeaderRepository : EfCoreRepository<MdmServiceDbContext, MCPHeader, Guid>, IMCPHeaderRepository
    {
        public EfCoreMCPHeaderRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<MCPHeaderWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(mcpHeader => new MCPHeaderWithNavigationProperties
                {
                    MCPHeader = mcpHeader,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == mcpHeader.RouteId),
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == mcpHeader.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<MCPHeaderWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, routeId, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MCPHeaderConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<MCPHeaderWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from mcpHeader in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on mcpHeader.RouteId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join company in (await GetDbContextAsync()).Companies on mcpHeader.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new MCPHeaderWithNavigationProperties
                   {
                       MCPHeader = mcpHeader,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       Company = company
                   };
        }

        protected virtual IQueryable<MCPHeaderWithNavigationProperties> ApplyFilter(
            IQueryable<MCPHeaderWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.MCPHeader.Code.Contains(filterText) || e.MCPHeader.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.MCPHeader.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.MCPHeader.Name.Contains(name))
                    .WhereIf(effectiveDateMin.HasValue, e => e.MCPHeader.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.MCPHeader.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.MCPHeader.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.MCPHeader.EndDate <= endDateMax.Value)
                    .WhereIf(routeId != null && routeId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == routeId)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<MCPHeader>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MCPHeaderConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? routeId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, routeId, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MCPHeader> ApplyFilter(
            IQueryable<MCPHeader> query,
            string filterText,
            string code = null,
            string name = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value);
        }
    }
}