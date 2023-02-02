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

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial class EfCoreGeoMasterRepository : EfCoreRepository<MdmServiceDbContext, GeoMaster, Guid>, IGeoMasterRepository
    {
        public EfCoreGeoMasterRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<GeoMasterWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(geoMaster => new GeoMasterWithNavigationProperties
                {
                    GeoMaster = geoMaster,
                    GeoMaster1 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == geoMaster.ParentId)
                }).FirstOrDefault();
        }

        public async Task<List<GeoMasterWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, erpCode, name, levelMin, levelMax, parentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? GeoMasterConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<GeoMasterWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from geoMaster in (await GetDbSetAsync())
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on geoMaster.ParentId equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()

                   select new GeoMasterWithNavigationProperties
                   {
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1
                   };
        }

        protected virtual IQueryable<GeoMasterWithNavigationProperties> ApplyFilter(
            IQueryable<GeoMasterWithNavigationProperties> query,
            string filterText,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            Guid? parentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.GeoMaster.Code.Contains(filterText) || e.GeoMaster.ERPCode.Contains(filterText) || e.GeoMaster.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.GeoMaster.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.GeoMaster.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.GeoMaster.Name.Contains(name))
                    .WhereIf(levelMin.HasValue, e => e.GeoMaster.Level >= levelMin.Value)
                    .WhereIf(levelMax.HasValue, e => e.GeoMaster.Level <= levelMax.Value)
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.GeoMaster1 != null && e.GeoMaster1.Id == parentId);
        }

        public async Task<List<GeoMaster>> GetListAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, erpCode, name, levelMin, levelMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? GeoMasterConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, erpCode, name, levelMin, levelMax, parentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<GeoMaster> ApplyFilter(
            IQueryable<GeoMaster> query,
            string filterText,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.ERPCode.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(levelMin.HasValue, e => e.Level >= levelMin.Value)
                    .WhereIf(levelMax.HasValue, e => e.Level <= levelMax.Value);
        }
    }
}