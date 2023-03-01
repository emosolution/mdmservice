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
        Guid? companyId, Guid? systemDataId, int startNumber, string prefix, string suffix, int length)
        {
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength);

            var numberingConfig = new NumberingConfig(
             GuidGenerator.Create(),
             companyId, systemDataId, startNumber, prefix, suffix, length
             );

            return await _numberingConfigRepository.InsertAsync(numberingConfig);
        }

        public async Task<NumberingConfig> UpdateAsync(
            Guid id,
            Guid? companyId, Guid? systemDataId, int startNumber, string prefix, string suffix, int length, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength);

            var numberingConfig = await _numberingConfigRepository.GetAsync(id);

            numberingConfig.CompanyId = companyId;
            numberingConfig.SystemDataId = systemDataId;
            numberingConfig.StartNumber = startNumber;
            numberingConfig.Prefix = prefix;
            numberingConfig.Suffix = suffix;
            numberingConfig.Length = length;

            numberingConfig.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _numberingConfigRepository.UpdateAsync(numberingConfig);
        }

    }
}