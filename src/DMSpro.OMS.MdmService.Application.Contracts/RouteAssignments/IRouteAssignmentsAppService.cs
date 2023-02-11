using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public partial interface IRouteAssignmentsAppService : IApplicationService
    {
        Task<PagedResultDto<RouteAssignmentWithNavigationPropertiesDto>> GetListAsync(GetRouteAssignmentsInput input);

        Task<RouteAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<RouteAssignmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<RouteAssignmentDto> CreateAsync(RouteAssignmentCreateDto input);

        Task<RouteAssignmentDto> UpdateAsync(Guid id, RouteAssignmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteAssignmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}