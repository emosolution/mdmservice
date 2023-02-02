using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial interface ICustomerGroupByGeoRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}