using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
    public partial interface IItemsAppService
    {
        Task<ItemProfileDto> GetItemProfileAsync(Guid id);

        Task<string> GetInfoForSOAsync(Guid companyId,
            DateTime? lastItemInfoUpdate, DateTime? lastCustomerInfoUpdate,
            DateTime? lastRouteInfoUpdate, DateTime? lastVendorInfoUpdate,
            bool getCustomerInfo, bool getVendorInfo, bool getRouteInfo);
    }
}
