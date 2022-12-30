using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public interface IUOMGroupDetailRepository : IRepository<UOMGroupDetail, Guid>
    {
        Task<UOMGroupDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<UOMGroupDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            Guid? uOMGroupId = null,
            Guid? altUOMId = null,
            Guid? baseUOMId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<UOMGroupDetail>> GetListAsync(
                    string filterText = null,
                    uint? altQtyMin = null,
                    uint? altQtyMax = null,
                    uint? baseQtyMin = null,
                    uint? baseQtyMax = null,
                    bool? active = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            Guid? uOMGroupId = null,
            Guid? altUOMId = null,
            Guid? baseUOMId = null,
            CancellationToken cancellationToken = default);
    }
}