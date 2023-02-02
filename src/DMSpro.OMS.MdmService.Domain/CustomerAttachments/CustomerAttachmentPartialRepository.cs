using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public partial interface ICustomerAttachmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}