using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public interface ISalesOrgHierarchiesInternalAppService : IApplicationService
    {
        Task<SalesOrgHierarchyDto> CreateAsync(Guid salesOrgHeaderId, Guid? parentId, 
            string code, string name, bool isRoute, bool isSellingZone, bool active);

        Task ValidateOrganizationUnitAsync(Guid? id, string name,
            Guid? parentId, Guid saleOrgId);
    }
}
