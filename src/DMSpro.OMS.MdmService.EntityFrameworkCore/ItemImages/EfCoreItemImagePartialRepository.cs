using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemImages
{
	public partial class EfCoreItemImageRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}