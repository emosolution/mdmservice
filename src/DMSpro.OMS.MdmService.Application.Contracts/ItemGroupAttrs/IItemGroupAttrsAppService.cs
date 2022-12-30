using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public interface IItemGroupAttrsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupAttrWithNavigationPropertiesDto>> GetListAsync(GetItemGroupAttrsInput input);

        Task<ItemGroupAttrWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemGroupAttrDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemGroupAttrDto> CreateAsync(ItemGroupAttrCreateDto input);

        Task<ItemGroupAttrDto> UpdateAsync(Guid id, ItemGroupAttrUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupAttrExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}