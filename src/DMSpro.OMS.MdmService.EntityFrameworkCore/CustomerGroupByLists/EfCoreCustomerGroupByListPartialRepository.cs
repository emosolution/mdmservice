using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public partial class EfCoreCustomerGroupByListRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}