using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial interface IPriceListsAppService : IApplicationService
    {
        Task<PriceListDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PriceListDto> CreateAsync(PriceListCreateDto input);

        Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input);
    }
}