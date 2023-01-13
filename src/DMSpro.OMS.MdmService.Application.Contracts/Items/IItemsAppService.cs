using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Items
{
    public interface IItemsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemWithNavigationPropertiesDto>> GetListAsync(GetItemsInput input);

        Task<ItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupDetailLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemDto> CreateAsync(ItemCreateDto input);

        Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}