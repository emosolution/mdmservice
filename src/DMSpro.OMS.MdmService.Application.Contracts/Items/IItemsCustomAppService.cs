using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
    public partial interface IItemsAppService
    {
        Task<ItemProfileDto> GetItemProfileAsync(Guid id);

        Task<string> GetSOInfoAsync(Guid companyId, DateTime postingDate,
            DateTime? lastUpdateDate);
    }
}
