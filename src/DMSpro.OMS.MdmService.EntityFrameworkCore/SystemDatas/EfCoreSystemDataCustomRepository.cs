using DMSpro.OMS.MdmService.NumberingConfigs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemDatas
{
	public partial class EfCoreSystemDataRepository
	{
		public virtual async Task<List<SystemData>> GetNumberingConfigsSystemDataAsync()
		{
            var items =
                    await GetListAsync(x => x.Code == NumberingConfigConsts.SystemDataCode);
            return items;
        }
    }
}