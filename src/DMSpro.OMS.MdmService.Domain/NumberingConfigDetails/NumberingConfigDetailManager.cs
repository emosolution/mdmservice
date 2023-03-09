using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailManager : DomainService
    {
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;

        public NumberingConfigDetailManager(INumberingConfigDetailRepository numberingConfigDetailRepository)
        {
            _numberingConfigDetailRepository = numberingConfigDetailRepository;
        }

        public async Task<NumberingConfigDetail> CreateAsync(
        Guid numberingConfigId, Guid companyId, bool active, string description)
        {
            Check.NotNull(numberingConfigId, nameof(numberingConfigId));
            Check.NotNull(companyId, nameof(companyId));
            Check.Length(description, nameof(description), NumberingConfigDetailConsts.DescriptionMaxLength);

            var numberingConfigDetail = new NumberingConfigDetail(
             GuidGenerator.Create(),
             numberingConfigId, companyId, active, description
             );

            return await _numberingConfigDetailRepository.InsertAsync(numberingConfigDetail);
        }

        public async Task<NumberingConfigDetail> UpdateAsync(
            Guid id,
            Guid numberingConfigId, Guid companyId, bool active, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(numberingConfigId, nameof(numberingConfigId));
            Check.NotNull(companyId, nameof(companyId));
            Check.Length(description, nameof(description), NumberingConfigDetailConsts.DescriptionMaxLength);

            var numberingConfigDetail = await _numberingConfigDetailRepository.GetAsync(id);

            numberingConfigDetail.NumberingConfigId = numberingConfigId;
            numberingConfigDetail.CompanyId = companyId;
            numberingConfigDetail.Active = active;
            numberingConfigDetail.Description = description;

            numberingConfigDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _numberingConfigDetailRepository.UpdateAsync(numberingConfigDetail);
        }

    }
}