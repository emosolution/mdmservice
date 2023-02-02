using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public partial interface IEmployeeInZoneRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}