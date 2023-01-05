using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public interface IItemGroupListsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupListWithNavigationPropertiesDto>> GetListAsync(GetItemGroupListsInput input);

        Task<ItemGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ItemGroupListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input);

        Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupListExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
    }
}