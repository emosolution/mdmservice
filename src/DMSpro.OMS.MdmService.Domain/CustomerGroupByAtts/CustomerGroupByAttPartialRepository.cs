using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
	public partial interface ICustomerGroupByAttRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}