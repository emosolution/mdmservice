using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial class EfCoreItemAttributeRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}