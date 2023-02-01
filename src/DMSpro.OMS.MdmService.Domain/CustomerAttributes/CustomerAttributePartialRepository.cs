using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial interface ICustomerAttributeRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}