using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp;
using System;
using System.Linq;
using System.Text.Json;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    
    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigsAppService
    {
        private const string _NUMBERING_CONFIG_SYSTEM_DATA_CODE = "SY01";
        private Dictionary<string, string> _NUMBERING_CONFIG_VALUE_CODE_DICTIONARY = new()
        {
            {"Customer", "M1" },
            {"Employee", "M2" },
            {"SalesOrgHierarchy", "M3" },
            {"SalesRequest", "S0" },
        };

        /*
        private async Task<List<NumberingConfig>> GetConfigsOfValueCode(string valueCode)
        {
            var data = await _systemDataRepository.GetListAsync(x => x.Code == _NUMBERING_CONFIG_SYSTEM_DATA_CODE &&
                x.ValueCode == valueCode);
            if (data.Count != 1)
            {
                var detailDict = new Dictionary<string, string> { ["valueCode"] = valueCode };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["rror:NumberingConfigsAppService:550"], 
                    code: "1", details: detailString);
            }
            Guid dataId = data.First().Id;
            var configs = await _numberingConfigRepository.GetListAsync(x => x.SystemDataId == dataId && 
                x.Active == true);
            if (configs.Count < 0) {
                throw new BusinessException(message: L["rror:NumberingConfigsAppService:551"], code: "1");
            }
            return configs;
        }
        */
        /*
        private async Task<NumberingConfig> GetConfigOfCompanyAndValueCode(Guid companyId, string valueCode)
        {
            var configs = await GetConfigsOfValueCode(valueCode);
            List<Guid> configIds = configs.Select(x => x.Id).Distinct().ToList();
            var details = await _numberingConfigDetailRepository.GetListAsync(x => x.CompanyId == companyId &&
                configIds.Contains(x.NumberingConfigId));
            if (details.Count == 1)
            {
                var detail = details.First();
                if (!detail.Active)
                {

                }
                var config = configs.First(x => x.Id == details.First().NumberingConfigId);
                if (config == null)
                {
                    throw new BusinessException(message: L["rror:NumberingConfigsAppService:552"], code: "1");
                }
                else
                {
                    return config;
                }
            }
        }
        */
    }
}