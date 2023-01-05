using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public interface IItemGroupAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupAttributeWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttributesInput input);

        Task<ItemGroupAttributeWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ItemGroupAttributeDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input);

        Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}