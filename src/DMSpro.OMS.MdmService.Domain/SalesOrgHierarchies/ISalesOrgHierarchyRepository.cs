using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial interface ISalesOrgHierarchyRepository : IRepository<SalesOrgHierarchy, Guid>
    {
        Task<SalesOrgHierarchyWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<SalesOrgHierarchyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            Guid? salesOrgHeaderId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<SalesOrgHierarchy>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string name = null,
                    int? levelMin = null,
                    int? levelMax = null,
                    bool? isRoute = null,
                    bool? isSellingZone = null,
                    string hierarchyCode = null,
                    bool? active = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            int? levelMin = null,
            int? levelMax = null,
            bool? isRoute = null,
            bool? isSellingZone = null,
            string hierarchyCode = null,
            bool? active = null,
            Guid? salesOrgHeaderId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default);
        

    }
}