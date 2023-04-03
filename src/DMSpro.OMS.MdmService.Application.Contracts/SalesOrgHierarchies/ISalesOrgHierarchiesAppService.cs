using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial interface ISalesOrgHierarchiesAppService : IApplicationService
    {
        Task<SalesOrgHierarchyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesOrgHierarchyDto> CreateRootAsync(SalesOrgHierarchyCreateRootDto input);

        Task<SalesOrgHierarchyDto> CreateSubAsync(SalesOrgHierarchyCreateSubDto input);

        Task<SalesOrgHierarchyDto> CreateRouteAsync(SalesOrgHierarchyCreateRouteDto input);

        Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input);
    }
}