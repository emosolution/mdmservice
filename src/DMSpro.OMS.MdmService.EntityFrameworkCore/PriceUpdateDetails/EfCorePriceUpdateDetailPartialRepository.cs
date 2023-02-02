using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial class EfCorePriceUpdateDetailRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}