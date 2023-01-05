using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public interface IPriceListDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<PriceListDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceListDetailsInput input);

        Task<PriceListDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<PriceListDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PriceListDetailDto> CreateAsync(PriceListDetailCreateDto input);

        Task<PriceListDetailDto> UpdateAsync(Guid id, PriceListDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}