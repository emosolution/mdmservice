using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public partial class EfCoreItemAttachmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}