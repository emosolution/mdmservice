using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial class EfCorePricelistAssignmentRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}