using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public interface IProdAttributeValuesAppService : IApplicationService
    {
        Task<PagedResultDto<ProdAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetProdAttributeValuesInput input);

        Task<ProdAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ProdAttributeValueDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetProductAttributeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ProdAttributeValueDto> CreateAsync(ProdAttributeValueCreateDto input);

        Task<ProdAttributeValueDto> UpdateAsync(Guid id, ProdAttributeValueUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProdAttributeValueExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}