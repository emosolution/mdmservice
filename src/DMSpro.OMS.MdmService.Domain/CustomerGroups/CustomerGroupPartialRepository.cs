using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
	public partial interface ICustomerGroupRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}