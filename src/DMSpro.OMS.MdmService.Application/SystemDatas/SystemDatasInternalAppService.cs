using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.MdmService.NumberingConfigs;
using Google.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial class SystemDatasInternalAppService : ApplicationService, ISystemDatasInternalAppService
    {
        private readonly ISystemDataRepository _systemDataRepository;

        public SystemDatasInternalAppService(ISystemDataRepository systemDataRepository)
        {
            _systemDataRepository = systemDataRepository;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public virtual async Task<SystemDataDto> GetNumberConfigSystemDataByValueName(string valueName)
        {
            using (CurrentTenant.Change(null))
            {
                var items = await _systemDataRepository.GetListAsync(
                    x => x.Code == NumberingConfigConsts.SystemDataCode &&
                    x.ValueName == valueName);
                if (items.Count != 1)
                {
                    var detailDict = new Dictionary<string, string>
                    {
                        ["code"] = NumberingConfigConsts.SystemDataCode,
                        ["valueName"] = valueName,
                    };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: L["Error:SystemData:550"],
                        code: "1", details: detailString);
                }
                return ObjectMapper.Map<SystemData, SystemDataDto>(items.First());
            }
        }

        public virtual async Task<SystemDataDto> GetSystemDataByCodeAndValueName(string code, string valueName)
        {
            var items = await _systemDataRepository.GetListAsync(x => x.Code == code &&
            x.ValueName == valueName);
            if (items.Count != 1)
            {
                var detailDict = new Dictionary<string, string>
                {
                    ["code"] = code,
                    ["valueName"] = valueName,
                };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:SystemData:550"],
                    code: "1", details: detailString);
            }
            return ObjectMapper.Map<SystemData, SystemDataDto>(items.First());
        }

        public virtual async Task<List<SystemDataDto>> GetNumberingConfigsSystemData()
        {
            using (CurrentTenant.Change(null))
            {
                var items =
                    await _systemDataRepository.GetListAsync(x => x.Code == NumberingConfigConsts.SystemDataCode);
                return ObjectMapper.Map<List<SystemData>, List<SystemDataDto>>(items);
            }
        }
    }
}
