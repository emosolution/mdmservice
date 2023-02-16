using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public partial interface ICustomerGroupByListRepository
	{
		Task<List<CustomerGroupByList>> GetByIdAsync(List<Guid> ids);
    }
}