using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public partial interface IItemAttachmentRepository
	{
		Task<List<ItemAttachment>> GetByIdAsync(List<Guid> ids);
    }
}