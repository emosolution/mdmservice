using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	public partial interface IEmployeeAttachmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}