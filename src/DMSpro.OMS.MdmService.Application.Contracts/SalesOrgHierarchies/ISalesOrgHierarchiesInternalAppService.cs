using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public interface ISalesOrgHierarchiesInternalAppService : IApplicationService
    {
        Task<SalesOrgHierarchyWithTenantDto> GetWithTenantIdAsynce(Guid id);
    }
}
