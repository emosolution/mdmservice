using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public partial interface ICustomerGroupByListRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}