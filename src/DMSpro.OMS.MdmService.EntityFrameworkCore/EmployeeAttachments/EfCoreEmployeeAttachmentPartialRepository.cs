using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	public partial class EfCoreEmployeeAttachmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}