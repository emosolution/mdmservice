using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial class EfCoreCustomerGroupByGeoRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}