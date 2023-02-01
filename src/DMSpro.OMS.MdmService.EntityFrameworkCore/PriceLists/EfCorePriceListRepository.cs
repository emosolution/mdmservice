using DMSpro.OMS.MdmService.PriceLists;
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

namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial class EfCorePriceListRepository : EfCoreRepository<MdmServiceDbContext, PriceList, Guid>, IPriceListRepository
    {
        public EfCorePriceListRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<PriceListWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(priceList => new PriceListWithNavigationProperties
                {
                    PriceList = priceList,
                    PriceList1 = dbContext.PriceLists.FirstOrDefault(c => c.Id == priceList.BasePriceListId)
                }).FirstOrDefault();
        }

        public async Task<List<PriceListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            ArithmeticOperator? arithmeticOperation = null,
            int? arithmeticFactorMin = null,
            int? arithmeticFactorMax = null,
            ArithmeticFactorType? arithmeticFactorType = null,
            bool? isFirstPriceList = null,
            Guid? basePriceListId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, active, arithmeticOperation, arithmeticFactorMin, arithmeticFactorMax, arithmeticFactorType, isFirstPriceList, basePriceListId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceListConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PriceListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from priceList in (await GetDbSetAsync())
                   join priceList1 in (await GetDbContextAsync()).PriceLists on priceList.BasePriceListId equals priceList1.Id into priceLists1
                   from priceList1 in priceLists1.DefaultIfEmpty()

                   select new PriceListWithNavigationProperties
                   {
                       PriceList = priceList,
                       PriceList1 = priceList1
                   };
        }

        protected virtual IQueryable<PriceListWithNavigationProperties> ApplyFilter(
            IQueryable<PriceListWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            bool? active = null,
            ArithmeticOperator? arithmeticOperation = null,
            int? arithmeticFactorMin = null,
            int? arithmeticFactorMax = null,
            ArithmeticFactorType? arithmeticFactorType = null,
            bool? isFirstPriceList = null,
            Guid? basePriceListId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PriceList.Code.Contains(filterText) || e.PriceList.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.PriceList.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.PriceList.Name.Contains(name))
                    .WhereIf(active.HasValue, e => e.PriceList.Active == active)
                    .WhereIf(arithmeticOperation.HasValue, e => e.PriceList.ArithmeticOperation == arithmeticOperation)
                    .WhereIf(arithmeticFactorMin.HasValue, e => e.PriceList.ArithmeticFactor >= arithmeticFactorMin.Value)
                    .WhereIf(arithmeticFactorMax.HasValue, e => e.PriceList.ArithmeticFactor <= arithmeticFactorMax.Value)
                    .WhereIf(arithmeticFactorType.HasValue, e => e.PriceList.ArithmeticFactorType == arithmeticFactorType)
                    .WhereIf(isFirstPriceList.HasValue, e => e.PriceList.IsFirstPriceList == isFirstPriceList)
                    .WhereIf(basePriceListId != null && basePriceListId != Guid.Empty, e => e.PriceList1 != null && e.PriceList1.Id == basePriceListId);
        }

        public async Task<List<PriceList>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            ArithmeticOperator? arithmeticOperation = null,
            int? arithmeticFactorMin = null,
            int? arithmeticFactorMax = null,
            ArithmeticFactorType? arithmeticFactorType = null,
            bool? isFirstPriceList = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, active, arithmeticOperation, arithmeticFactorMin, arithmeticFactorMax, arithmeticFactorType, isFirstPriceList);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PriceListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            ArithmeticOperator? arithmeticOperation = null,
            int? arithmeticFactorMin = null,
            int? arithmeticFactorMax = null,
            ArithmeticFactorType? arithmeticFactorType = null,
            bool? isFirstPriceList = null,
            Guid? basePriceListId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, active, arithmeticOperation, arithmeticFactorMin, arithmeticFactorMax, arithmeticFactorType, isFirstPriceList, basePriceListId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PriceList> ApplyFilter(
            IQueryable<PriceList> query,
            string filterText,
            string code = null,
            string name = null,
            bool? active = null,
            ArithmeticOperator? arithmeticOperation = null,
            int? arithmeticFactorMin = null,
            int? arithmeticFactorMax = null,
            ArithmeticFactorType? arithmeticFactorType = null,
            bool? isFirstPriceList = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(arithmeticOperation.HasValue, e => e.ArithmeticOperation == arithmeticOperation)
                    .WhereIf(arithmeticFactorMin.HasValue, e => e.ArithmeticFactor >= arithmeticFactorMin.Value)
                    .WhereIf(arithmeticFactorMax.HasValue, e => e.ArithmeticFactor <= arithmeticFactorMax.Value)
                    .WhereIf(arithmeticFactorType.HasValue, e => e.ArithmeticFactorType == arithmeticFactorType)
                    .WhereIf(isFirstPriceList.HasValue, e => e.IsFirstPriceList == isFirstPriceList);
        }
    }
}