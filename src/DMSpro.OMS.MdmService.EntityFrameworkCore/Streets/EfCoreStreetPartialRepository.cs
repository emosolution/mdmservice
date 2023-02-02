using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Streets
{
	public partial class EfCoreStreetRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}