using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public interface ISalesOrgEmpAssignmentsAppService : IApplicationService
    {
        Task<PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgEmpAssignmentsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<SalesOrgEmpAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input);

        Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgEmpAssignmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}