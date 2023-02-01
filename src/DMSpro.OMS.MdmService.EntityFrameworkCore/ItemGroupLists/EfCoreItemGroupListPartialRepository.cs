using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public partial class EfCoreItemGroupListRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}