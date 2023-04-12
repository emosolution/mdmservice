using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial interface IPriceUpdatesAppService : IApplicationService
    {
        Task<PriceUpdateDto> GetAsync(Guid id);

        Task<PriceUpdateDto> CancelAsync(Guid id);

        Task<PriceUpdateDto> ReleaseAsync(Guid id);

        Task<PriceUpdateDto> CreateAsync(PriceUpdateCreateDto input);

        Task<PriceUpdateDto> UpdateAsync(Guid id, PriceUpdateUpdateDto input);
    }
}