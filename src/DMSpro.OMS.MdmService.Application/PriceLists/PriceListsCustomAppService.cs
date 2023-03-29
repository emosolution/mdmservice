using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.PriceLists
{

    public partial class PriceListsAppService 
    {
        [Authorize(MdmServicePermissions.PriceLists.Release)]
        public virtual async Task<PriceListDto> ReleaseAsync(Guid id)
        {
            var record  = await _priceListRepository.GetAsync(x => x.Id == id && 
                x.IsReleased == false && x.ReleasedDate == null);
            record.IsReleased= true;
            record.ReleasedDate = DateTime.Now;
            await _priceListRepository.UpdateAsync(record);
            return ObjectMapper.Map<PriceList, PriceListDto>(record);
        }
    }
}