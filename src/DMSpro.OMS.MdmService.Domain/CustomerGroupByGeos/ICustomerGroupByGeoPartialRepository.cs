using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial interface ICustomerGroupByGeoRepository
	{
		Task<List<CustomerGroupByGeo>> GetByIdAsync(List<Guid> ids);
    }
}