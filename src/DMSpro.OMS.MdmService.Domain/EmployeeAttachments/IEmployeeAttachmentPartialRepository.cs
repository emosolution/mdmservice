using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	public partial interface IEmployeeAttachmentRepository
	{
		Task<List<EmployeeAttachment>> GetByIdAsync(List<Guid> ids);
    }
}