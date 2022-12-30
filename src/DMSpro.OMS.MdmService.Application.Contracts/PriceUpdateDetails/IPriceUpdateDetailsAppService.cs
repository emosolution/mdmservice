using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public interface IPriceUpdateDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<PriceUpdateDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdateDetailsInput input);

        Task<PriceUpdateDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<PriceUpdateDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceUpdateLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceListDetailLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PriceUpdateDetailDto> CreateAsync(PriceUpdateDetailCreateDto input);

        Task<PriceUpdateDetailDto> UpdateAsync(Guid id, PriceUpdateDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}