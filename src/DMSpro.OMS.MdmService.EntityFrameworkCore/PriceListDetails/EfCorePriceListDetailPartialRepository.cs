using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public partial class EfCorePriceListDetailRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}