using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial interface ICustomerAttributePartialRepository
	{
		Task<List<CustomerAttribute>> GetByIdAsync(List<Guid> ids);
    }
}