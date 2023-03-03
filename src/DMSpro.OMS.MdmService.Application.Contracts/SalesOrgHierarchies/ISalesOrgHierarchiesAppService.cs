using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial interface ISalesOrgHierarchiesAppService : IApplicationService
    {
        Task<PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgHierarchiesInput input);

        Task<SalesOrgHierarchyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<SalesOrgHierarchyDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHeaderLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<SalesOrgHierarchyDto> CreateAsync(SalesOrgHierarchyCreateDto input);

        Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHierarchyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}