using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
	public partial interface ICustomerGroupByAttRepository
	{
		Task<List<CustomerGroupByAtt>> GetByIdAsync(List<Guid> ids);
    }
}