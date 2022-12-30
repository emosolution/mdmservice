using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public interface ICustomerAttachmentsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttachmentsInput input);

        Task<CustomerAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<CustomerAttachmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerAttachmentDto> CreateAsync(CustomerAttachmentCreateDto input);

        Task<CustomerAttachmentDto> UpdateAsync(Guid id, CustomerAttachmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttachmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}