using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
	public partial interface ICustomerContactRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}