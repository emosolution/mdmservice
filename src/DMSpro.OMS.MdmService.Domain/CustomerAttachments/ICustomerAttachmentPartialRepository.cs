using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public partial interface ICustomerAttachmentPartialRepository
	{
		Task<List<CustomerAttachment>> GetByIdAsync(List<Guid> ids);
    }
}