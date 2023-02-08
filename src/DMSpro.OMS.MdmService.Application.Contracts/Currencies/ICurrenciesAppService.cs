using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Currencies
{
    public partial interface ICurrenciesAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input);

        Task<CurrencyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CurrencyDto> CreateAsync(CurrencyCreateDto input);

        Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CurrencyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}