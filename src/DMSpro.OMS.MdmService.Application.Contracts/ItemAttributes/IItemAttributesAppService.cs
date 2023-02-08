using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;



namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public partial interface IItemAttributesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<ItemAttributeDto>> GetListAsync(GetItemAttributesInput input);

        Task<ItemAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input);

        Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}