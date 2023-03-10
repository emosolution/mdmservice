using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial interface ISystemDatasInternalAppService : IApplicationService
    {
        Task<SystemDataDto> GetSystemDataByCodeAndValueNameAsync(string code, string valueName);
    }
}
