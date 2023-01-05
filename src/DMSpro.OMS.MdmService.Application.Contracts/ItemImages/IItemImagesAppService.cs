using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public interface IItemImagesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemImageWithNavigationPropertiesDto>> GetListAsync(GetItemImagesInput input);

        Task<ItemImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ItemImageDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemImageDto> CreateAsync(ItemImageCreateDto input);

        Task<ItemImageDto> UpdateAsync(Guid id, ItemImageUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemImageExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
    }
}