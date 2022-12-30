using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public interface IPriceUpdateDetailRepository : IRepository<PriceUpdateDetail, Guid>
    {
        Task<PriceUpdateDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<PriceUpdateDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            Guid? priceUpdateId = null,
            Guid? priceListDetailId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<PriceUpdateDetail>> GetListAsync(
                    string filterText = null,
                    int? priceBeforeUpdateMin = null,
                    int? priceBeforeUpdateMax = null,
                    int? newPriceMin = null,
                    int? newPriceMax = null,
                    DateTime? updatedDateMin = null,
                    DateTime? updatedDateMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? priceBeforeUpdateMin = null,
            int? priceBeforeUpdateMax = null,
            int? newPriceMin = null,
            int? newPriceMax = null,
            DateTime? updatedDateMin = null,
            DateTime? updatedDateMax = null,
            Guid? priceUpdateId = null,
            Guid? priceListDetailId = null,
            CancellationToken cancellationToken = default);
    }
}