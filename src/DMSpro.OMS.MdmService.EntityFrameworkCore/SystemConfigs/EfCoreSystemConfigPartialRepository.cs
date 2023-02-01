using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
	public partial class EfCoreSystemConfigRepository
	{
		public virtual async Task<Guid?> GetIdByCodeAsync(string code)
        {
            var item = (await GetDbSetAsync()).Where(x => x.Code == code).FirstOrDefault();
            return item?.Id;
        }
    }
}