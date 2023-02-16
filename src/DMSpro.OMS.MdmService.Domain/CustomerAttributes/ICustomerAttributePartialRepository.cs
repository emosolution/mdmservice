using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial interface ICustomerAttributeRepository
	{
		Task<List<CustomerAttribute>> GetByIdAsync(List<Guid> ids);
    }
}