using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Items
{
    public partial interface IItemsAppService
    {
        Task<ItemProfileDto> GetItemProfile(Guid id);
    }
}
