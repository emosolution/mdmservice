using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
	public partial class EfCoreCustomerGroupByAttRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}