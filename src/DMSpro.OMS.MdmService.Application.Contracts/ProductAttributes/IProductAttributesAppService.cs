using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public interface IProductAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<ProductAttributeDto>> GetListAsync(GetProductAttributesInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ProductAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ProductAttributeDto> CreateAsync(ProductAttributeCreateDto input);

        Task<ProductAttributeDto> UpdateAsync(Guid id, ProductAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}