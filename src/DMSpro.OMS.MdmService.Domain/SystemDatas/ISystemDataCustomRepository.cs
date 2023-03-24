using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial interface ISystemDataRepository
    {
        Task<List<SystemData>> GetNumberingConfigsSystemDataAsync();
    }
}
