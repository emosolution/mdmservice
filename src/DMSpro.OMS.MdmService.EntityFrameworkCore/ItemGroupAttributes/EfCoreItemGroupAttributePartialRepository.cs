using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
	public partial class EfCoreItemGroupAttributeRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}