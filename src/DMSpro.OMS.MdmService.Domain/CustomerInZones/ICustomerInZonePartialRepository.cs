using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public partial interface ICustomerInZoneRepository
	{
		Task<List<CustomerInZone>> GetByIdAsync(List<Guid> ids);
    }
}