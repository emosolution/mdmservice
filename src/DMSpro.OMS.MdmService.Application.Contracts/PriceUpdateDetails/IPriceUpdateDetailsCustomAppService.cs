using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public partial interface IPriceUpdateDetailsAppService : IApplicationService
    {
        Task<PriceUpdateDetailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PriceUpdateDetailDto> CreateAsync(PriceUpdateDetailCreateDto input);

        Task<PriceUpdateDetailDto> UpdateAsync(Guid id, PriceUpdateDetailUpdateDto input);
    }
}