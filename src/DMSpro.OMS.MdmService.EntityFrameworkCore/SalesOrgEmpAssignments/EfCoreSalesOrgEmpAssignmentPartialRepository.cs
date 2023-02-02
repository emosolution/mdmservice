using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public partial class EfCoreSalesOrgEmpAssignmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}