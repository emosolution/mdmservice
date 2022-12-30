using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public interface IItemGroupListsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupListWithNavigationPropertiesDto>> GetListAsync(GetItemGroupListsInput input);

        Task<ItemGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemGroupListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemMasterLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input);

        Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupListExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}