using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public partial interface IPriceListDetailRepository : IRepository<PriceListDetail, Guid>
    {
        Task<PriceListDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<PriceListDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            decimal? basedOnPriceMin = null,
            decimal? basedOnPriceMax = null,
            string description = null,
            Guid? priceListId = null,
            Guid? uOMId = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<PriceListDetail>> GetListAsync(
                    string filterText = null,
                    decimal? priceMin = null,
                    decimal? priceMax = null,
                    decimal? basedOnPriceMin = null,
                    decimal? basedOnPriceMax = null,
                    string description = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            decimal? basedOnPriceMin = null,
            decimal? basedOnPriceMax = null,
            string description = null,
            Guid? priceListId = null,
            Guid? uOMId = null,
            Guid? itemId = null,
            CancellationToken cancellationToken = default);
    }
}