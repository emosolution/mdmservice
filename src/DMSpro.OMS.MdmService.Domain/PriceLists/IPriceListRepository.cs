using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial interface IPriceListRepository : IRepository<PriceList, Guid>
    {
        Task<PriceListWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<PriceListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<PriceList>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}