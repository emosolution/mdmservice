using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public partial interface IItemAttachmentRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}