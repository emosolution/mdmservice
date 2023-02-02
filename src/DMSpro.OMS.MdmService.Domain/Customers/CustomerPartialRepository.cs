using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Customers
{
	public partial interface ICustomerRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}