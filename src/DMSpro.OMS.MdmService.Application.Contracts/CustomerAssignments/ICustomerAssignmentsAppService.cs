using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public partial interface ICustomerAssignmentsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<CustomerAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAssignmentsInput input);

        Task<CustomerAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

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