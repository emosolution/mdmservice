using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
	public partial class EfCorePriceUpdateRepository
	{
		public virtual async Task<Guid?> GetIdByCodeAsync(string code)
        {
            var item = (await GetDbSetAsync()).Where(x => x.Code == code).FirstOrDefault();
            return item?.Id;
        }
    }
}