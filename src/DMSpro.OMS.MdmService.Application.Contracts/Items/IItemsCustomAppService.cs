using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
    public partial interface IItemsAppService
    {
        Task<ItemProfileDto> GetItemProfileAsync(Guid id);

        Task<string> GetInfoForSOAsync(Guid companyId, 
            DateTime? lastApiDate, bool getRouteInfo);
    }
}
