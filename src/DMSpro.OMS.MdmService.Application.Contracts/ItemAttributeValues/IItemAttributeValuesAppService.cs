using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public interface IItemAttributeValuesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetItemAttributeValuesInput input);

        Task<ItemAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemAttributeValueDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemAttributeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeValueDto> CreateAsync(ItemAttributeValueCreateDto input);

        Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeValueExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}