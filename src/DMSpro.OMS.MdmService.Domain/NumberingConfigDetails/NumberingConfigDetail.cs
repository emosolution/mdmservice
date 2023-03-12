using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using DMSpro.OMS.MdmService.NumberingConfigs;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial class NumberingConfigDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        [CanBeNull]
        public virtual string Prefix { get; set; }

        public virtual int PaddingZeroNumber { get; set; }

        [CanBeNull]
        public virtual string Suffix { get; set; }

        public virtual bool Active { get; set; }

        public virtual int CurrentNumber { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public NumberingConfigDetail()
        {

        }

        public NumberingConfigDetail(Guid id, Guid numberingConfigId, Guid companyId, 
            string description, string prefix, int paddingZeroNumber, 
            string suffix, bool active, int currentNumber)
        {
            Id = id;
            Check.Length(description, nameof(description), NumberingConfigConsts.DescriptionMaxLength, 0);
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength, 0);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength, 0);
            Check.Range(paddingZeroNumber, nameof(paddingZeroNumber), 
                NumberingConfigConsts.PaddingZeroNumberMinValue, 
                NumberingConfigConsts.PaddingZeroNumberMaxValue);
            Check.Range(currentNumber, nameof(currentNumber),
                NumberingConfigDetailConsts.CurrentNumberMinValue);
            Description = description;
            Prefix = prefix;
            PaddingZeroNumber = paddingZeroNumber;
            Suffix = suffix;
            Active = active;
            CurrentNumber = currentNumber;
            NumberingConfigId = numberingConfigId;
            CompanyId = companyId;
        }

    }
}