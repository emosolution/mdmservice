using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial class NumberingConfig : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Prefix { get; set; }

        [CanBeNull]
        public virtual string Suffix { get; set; }

        public virtual int PaddingZeroNumber { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid? SystemDataId { get; set; }
        
        public virtual SystemData SystemData { get; set; }
        public NumberingConfig()
        {

        }

        public NumberingConfig(Guid id, Guid? systemDataId, string prefix, string suffix, int paddingZeroNumber, string description)
        {

            Id = id;
            Check.Length(prefix, nameof(prefix), NumberingConfigConsts.PrefixMaxLength, 0);
            Check.Length(suffix, nameof(suffix), NumberingConfigConsts.SuffixMaxLength, 0);
            Check.Length(description, nameof(description), NumberingConfigConsts.DescriptionMaxLength, 0);
            Prefix = prefix;
            Suffix = suffix;
            PaddingZeroNumber = paddingZeroNumber;
            Description = description;
            SystemDataId = systemDataId;
        }

    }
}