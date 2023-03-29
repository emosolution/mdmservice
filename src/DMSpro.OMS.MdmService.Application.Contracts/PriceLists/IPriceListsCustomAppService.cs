using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public partial interface IPriceListsAppService
    {
        Task<PriceListDto> ReleaseAsync(Guid id);
    }
}