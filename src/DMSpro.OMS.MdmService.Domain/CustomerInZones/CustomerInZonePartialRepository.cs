using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public partial interface ICustomerInZoneRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}