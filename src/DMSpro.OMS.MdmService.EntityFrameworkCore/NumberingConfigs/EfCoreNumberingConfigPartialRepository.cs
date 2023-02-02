using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial class EfCoreNumberingConfigRepository
	{
		public virtual Task<Guid?> GetIdByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}