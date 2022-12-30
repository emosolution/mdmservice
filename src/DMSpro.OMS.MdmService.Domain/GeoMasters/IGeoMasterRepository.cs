using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public interface IGeoMasterRepository : IRepository<GeoMaster, Guid>
    {
        Task<GeoMasterWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<GeoMasterWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<List<GeoMaster>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string erpCode = null,
                    string name = null,
                    int? levelMin = null,
                    int? levelMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default);
    }
}