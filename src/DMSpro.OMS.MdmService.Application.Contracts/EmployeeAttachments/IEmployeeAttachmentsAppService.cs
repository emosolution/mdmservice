using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial interface IEmployeeAttachmentsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAttachmentsInput input);

        Task<EmployeeAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<EmployeeAttachmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<EmployeeAttachmentDto> CreateAsync(EmployeeAttachmentCreateDto input);

        Task<EmployeeAttachmentDto> UpdateAsync(Guid id, EmployeeAttachmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeAttachmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}