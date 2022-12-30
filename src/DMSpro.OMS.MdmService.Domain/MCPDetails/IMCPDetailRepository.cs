using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public interface IMCPDetailRepository : IRepository<MCPDetail, Guid>
    {
        Task<MCPDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<MCPDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            Guid? customerId = null,
            Guid? mCPHeaderId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<MCPDetail>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    int? distanceMin = null,
                    int? distanceMax = null,
                    int? visitOrderMin = null,
                    int? visitOrderMax = null,
                    bool? monday = null,
                    bool? tuesday = null,
                    bool? wednesday = null,
                    bool? thursday = null,
                    bool? friday = null,
                    bool? saturday = null,
                    bool? sunday = null,
                    bool? week1 = null,
                    bool? week2 = null,
                    bool? week3 = null,
                    bool? week4 = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            bool? monday = null,
            bool? tuesday = null,
            bool? wednesday = null,
            bool? thursday = null,
            bool? friday = null,
            bool? saturday = null,
            bool? sunday = null,
            bool? week1 = null,
            bool? week2 = null,
            bool? week3 = null,
            bool? week4 = null,
            Guid? customerId = null,
            Guid? mCPHeaderId = null,
            CancellationToken cancellationToken = default);
    }
}