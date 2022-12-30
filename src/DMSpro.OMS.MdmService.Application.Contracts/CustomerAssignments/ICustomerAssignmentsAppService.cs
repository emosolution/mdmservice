using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public interface ICustomerAssignmentsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAssignmentsInput input);

        Task<CustomerAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerAssignmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerAssignmentDto> CreateAsync(CustomerAssignmentCreateDto input);

        Task<CustomerAssignmentDto> UpdateAsync(Guid id, CustomerAssignmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAssignmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}