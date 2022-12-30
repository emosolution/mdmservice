using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ItemMasters
{
    public interface IItemMastersAppService : IApplicationService
    {
        Task<PagedResultDto<ItemMasterWithNavigationPropertiesDto>> GetListAsync(GetItemMastersInput input);

        Task<ItemMasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemMasterDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetVATLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemMasterDto> CreateAsync(ItemMasterCreateDto input);

        Task<ItemMasterDto> UpdateAsync(Guid id, ItemMasterUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemMasterExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}