using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.MdmService.NumberingConfigs;
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

            LocalizationResource = typeof(MdmServiceResource);
        }

        public virtual async Task<SystemDataDto> GetNumberConfigSystemDataByValueName(string valueName)
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
                throw new UserFriendlyException(message: L["Error:SystemData:550"],
                    code: "1", details: detailString);
            }
            return ObjectMapper.Map<SystemData, SystemDataDto>(items.First());
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
                throw new UserFriendlyException(message: L["Error:SystemData:550"],
                    code: "1", details: detailString);
            }
            return ObjectMapper.Map<SystemData, SystemDataDto>(items.First());
        }

        public virtual async Task<List<SystemDataDto>> GetNumberingConfigsSystemData()
        {
            var items =
                await _systemDataRepository.GetNumberingConfigsSystemDataAsync();
            return ObjectMapper.Map<List<SystemData>, List<SystemDataDto>>(items);
        }

        public virtual async Task CreateAllForTenantAsync(
            List<Guid> tenantIds)
        {
            foreach (var tenantId in tenantIds)
            {
                await CreateAllForATenantAsync(tenantId);
            }
        }

        public virtual async Task CreateAllForHostAsync()
        {
            await CreateAllForATenantAsync(null);
        }

        private async Task CreateAllForATenantAsync(Guid? tenantId)
        {
            using (CurrentTenant.Change(tenantId))
            {
                var seedCodes = SystemDataConsts.SeedData.Select(x => x.Code).ToList();
                var seedValueCodes = SystemDataConsts.SeedData.Select(x => x.ValueCode).ToList();
                var seedValueNames = SystemDataConsts.SeedData.Select(x => x.ValueName).ToList();

                var existingRecords = await _systemDataRepository.GetListAsync(
                    x => seedCodes.Contains(x.Code) &&
                    seedValueCodes.Contains(x.ValueCode) &&
                    seedValueNames.Contains(x.ValueName));

                List<SystemData> seedSystemData = new();
                foreach (var (Code, ValueCode, ValueName) in SystemDataConsts.SeedData)
                {
                    if (existingRecords.Any(x => x.Code == Code &&
                        x.ValueCode == ValueCode && x.ValueName == ValueName))
                    {
                        continue;
                    }
                    SystemData seed = new(id: GuidGenerator.Create(),
                        code: Code, valueCode: ValueCode, valueName: ValueName);
                    seedSystemData.Add(seed);
                }
                await _systemDataRepository.InsertManyAsync(seedSystemData);
            }
        }
    }
}
