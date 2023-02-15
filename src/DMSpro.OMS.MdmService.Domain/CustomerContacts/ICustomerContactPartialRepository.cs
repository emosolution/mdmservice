using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
	public partial interface ICustomerContactRepository
	{
		Task<List<CustomerContact>> GetByIdAsync(List<Guid> ids);
    }
}