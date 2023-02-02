using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial interface ISalesOrgHierarchyRepository : IRepository<SalesOrgHierarchy, Guid>
    {
		Task<List<SalesOrgHierarchy>> GetChildrenAsync(
        Guid? parentId,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
    
    

    Task<List<SalesOrgHierarchy>> GetAllChildrenWithParentCodeAsync(
        string code,
        Guid? parentId,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );

    Task<SalesOrgHierarchy> GetAsync(
        string displayName,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task<List<SalesOrgHierarchy>> GetListAsync(
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );

    Task<List<SalesOrgHierarchy>> GetListAsync(
        IEnumerable<Guid> ids,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
    Task RemoveAllMembersAsync(
        SalesOrgHierarchy organizationUnit,
        CancellationToken cancellationToken = default
    );

	}
}