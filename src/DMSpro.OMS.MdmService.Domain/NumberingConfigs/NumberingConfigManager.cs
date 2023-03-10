using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigManager : DomainService
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigManager(INumberingConfigRepository numberingConfigRepository)
        {
            _numberingConfigRepository = numberingConfigRepository;
        }

        public async Task<NumberingConfig> CreateAsync(
        Guid? systemDataId, string prefix, string suffix, int paddingZeroNumber, string description)
        {
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength);
            Check.Length(description, nameof(description), NumberingConfigConsts.DescriptionMaxLength);

            var numberingConfig = new NumberingConfig(
             GuidGenerator.Create(),
             systemDataId, prefix, suffix, paddingZeroNumber, description
             );

            return await _numberingConfigRepository.InsertAsync(numberingConfig);
        }

        public async Task<NumberingConfig> UpdateAsync(
            Guid id,
            Guid? systemDataId, string prefix, string suffix, int paddingZeroNumber, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength);
            Check.Length(description, nameof(description), NumberingConfigConsts.DescriptionMaxLength);

            var numberingConfig = await _numberingConfigRepository.GetAsync(id);

            numberingConfig.SystemDataId = systemDataId;
            numberingConfig.Prefix = prefix;
            numberingConfig.Suffix = suffix;
            numberingConfig.PaddingZeroNumber = paddingZeroNumber;
            numberingConfig.Description = description;

            numberingConfig.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _numberingConfigRepository.UpdateAsync(numberingConfig);
        }

    }
}