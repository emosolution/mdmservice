using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public partial interface IEmployeeInZoneRepository
	{
		Task<List<EmployeeInZone>> GetByIdAsync(List<Guid> ids);
    }
}