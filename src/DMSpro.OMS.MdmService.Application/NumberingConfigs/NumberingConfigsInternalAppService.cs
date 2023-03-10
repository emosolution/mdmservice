using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.SystemDatas;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial class NumberingConfigsInternalAppService : ApplicationService,
        INumberingConfigsInternalAppService
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly ISystemDatasInternalAppService _systemDatasInternalAppService;

        public NumberingConfigsInternalAppService(INumberingConfigRepository numberingConfigRepository,
            ISystemDatasInternalAppService systemDatasInternalAppService,
            ISystemDataRepository systemDataRepository)
        {
            _numberingConfigRepository = numberingConfigRepository;
            _systemDatasInternalAppService = systemDatasInternalAppService;

            LocalizationResource = typeof(MdmServiceResource);
        }

        [Authorize(MdmServicePermissions.MasterDataManipulators.CreateNumberConfigs)]
        public virtual async Task<NumberingConfigDto> Create(NumberingConfigCreateDto input)
        {
            (string prefix, int paddingZeroNumber, string suffix) = 
                GetPresetDataOfConfig(input.ObjectType);
            input.Prefix = input.Prefix == null ? prefix : input.Prefix;
            input.PaddingZeroNumber = input.PaddingZeroNumber == null ? paddingZeroNumber : input.PaddingZeroNumber;
            input.Suffix = input.Suffix == null ? suffix : input.Suffix;

            Check.Length(input.Prefix, nameof(input.Prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(input.Suffix, nameof(input.Suffix), NumberingConfigConsts.SuffixMaxLength);
            Check.Range((short)input.PaddingZeroNumber,
                nameof(input.PaddingZeroNumber), NumberingConfigConsts.PaddingZeroNumberMinValue);

            var systemData = await
                _systemDatasInternalAppService.GetNumberConfigSystemDataByValueName(input.ObjectType);
            
            var config = await _numberingConfigRepository.GetListAsync(x => x.SystemDataId == systemData.Id);
            if (config.Count > 1)
            {
                throw new BusinessException(message: L["Error:NumberingConfig:550"], code: "1");
            }
            if (config.Count > 0)
            {
                throw new BusinessException(message: L["Error:NumberingConfig:551"], code: "1");
            }

            string description = $"Numbering config for {input.ObjectType}";
            var numberingConfig = new NumberingConfig(
                GuidGenerator.Create(), systemData.Id,
                input.Prefix, input.Suffix, (int) input.PaddingZeroNumber,
                description
             );

            await _numberingConfigRepository.InsertAsync(numberingConfig);

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }

        [Authorize(MdmServicePermissions.MasterDataManipulators.CreateNumberConfigs)]
        public virtual async Task<List<NumberingConfigDto>> CreateAllConfigsForTenant(
            List<Guid> tenantIds)
        {
            List<NumberingConfigDto> result = new();
            foreach (var tenantId in tenantIds)
            {
                var dtos = await CreateAllConfigsForATenantAsync(tenantId);
                result.AddRange(dtos);
            }
            return result;
        }

        [Authorize(MdmServicePermissions.MasterDataManipulators.CreateNumberConfigs)]
        public virtual async Task<List<NumberingConfigDto>> CreateAllConfigsForHost()
        {
            List<NumberingConfigDto> result = new();
            var dtos = await CreateAllConfigsForATenantAsync(null);
            result.AddRange(dtos);
            return result;
        }

        private async Task<List<NumberingConfigDto>> CreateAllConfigsForATenantAsync(Guid? tenantId)
        {
            List<NumberingConfigDto> result = new();
            var configs = await _numberingConfigRepository.GetListAsync(x => x.TenantId == tenantId);
            var systemDatas =
                await _systemDatasInternalAppService.GetNumberingConfigsSystemData();
            foreach (var systemData in systemDatas)
            {
                string objectType = systemData.ValueName;
                var config = configs.Select(x => x.SystemDataId == systemData.Id);
                int count = config.Count();
                if (count == 1)
                {
                    continue;
                }
                if (count > 1)
                {
                    var detailDict = new Dictionary<string, string>
                    {
                        ["code"] = systemData.Code,
                        ["valueName"] = objectType,
                    };
                    string detailString = JsonSerializer.Serialize(detailDict).ToString();
                    throw new BusinessException(message: L["Error:SystemData:550"],
                        code: "1", details: detailString);
                }
                (string prefix, int paddingZeroNumber, string suffix) = GetPresetDataOfConfig(objectType);
                NumberingConfigCreateDto input = new()
                {
                    Prefix = prefix,
                    PaddingZeroNumber = paddingZeroNumber,
                    Suffix = suffix,
                    ObjectType = objectType,
                };
                using (CurrentTenant.Change(tenantId))
                {
                    var dto = await Create(input);
                    result.Add(dto);
                }
            }
            return result;
        }

        private static (string, int, string) GetPresetDataOfConfig(string objectType)
        {
            string prefix = NumberingConfigConsts.DefaultCommonPrefix;
            if (NumberingConfigConsts.PrefixDictionary.TryGetValue(objectType,
                out string dictionaryPrefix))
            {
                prefix = dictionaryPrefix;
                ;
            }
            int paddingZeroNumber = NumberingConfigConsts.DefaultCommonPaddingZeroNumber;
            if (NumberingConfigConsts.PaddingZeroNumberDictionary.TryGetValue(objectType,
                out int dictionaryPaddingZeroNumber))
            {
                paddingZeroNumber = dictionaryPaddingZeroNumber;
            }
            string suffix = NumberingConfigConsts.DefaultCommonSuffix;
            if (NumberingConfigConsts.SuffixDictionary.TryGetValue(objectType,
                out string dictionarySuffix))
            {
                suffix = dictionarySuffix;
            }
            return (prefix, paddingZeroNumber, suffix);
        }
    }
}
