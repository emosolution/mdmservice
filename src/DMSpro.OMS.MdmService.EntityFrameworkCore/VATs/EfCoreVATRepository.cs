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

namespace DMSpro.OMS.MdmService.VATs
{
    public partial class EfCoreVATRepository : EfCoreRepository<MdmServiceDbContext, VAT, Guid>, IVATRepository
    {
        public EfCoreVATRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<VAT>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? rateMin = null,
            uint? rateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, rateMin, rateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VATConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? rateMin = null,
            uint? rateMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name, rateMin, rateMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<VAT> ApplyFilter(
            IQueryable<VAT> query,
            string filterText,
            string code = null,
            string name = null,
            uint? rateMin = null,
            uint? rateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(rateMin.HasValue, e => e.Rate >= rateMin.Value)
                    .WhereIf(rateMax.HasValue, e => e.Rate <= rateMax.Value);
        }
    }
}