using DMSpro.OMS.MdmService.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial interface IItemAttachmentsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>> GetListAsync(GetItemAttachmentsInput input);

        Task<ItemAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ItemAttachmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input);

        Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input);

        Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttachmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}