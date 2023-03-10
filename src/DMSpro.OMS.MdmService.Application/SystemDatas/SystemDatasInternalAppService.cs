using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial class SystemDatasInternalAppService : ApplicationService, ISystemDatasInternalAppService
    {
        private readonly ISystemDataRepository _systemDataRepository;

        public SystemDatasInternalAppService(ISystemDataRepository systemDataRepository)
        {
            _systemDataRepository = systemDataRepository;
        }

        public async Task<SystemDataDto> GetSystemDataByCodeAndValueNameAsync(string code, string valueName)
        {
            var items = await _systemDataRepository.GetListAsync(x => x.Code == code &&
                x.ValueName == valueName);
            if (items.Count != 1)
            {
                var detailDict = new Dictionary<string, string> 
                {
                    ["code"] = code,
                    ["valueCode"] = valueName,
                };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: "Error:SystemData:550", 
                    code: "1", details: detailString);
            }
            return ObjectMapper.Map<SystemData, SystemDataDto>(items.First());
        }
    }
}
