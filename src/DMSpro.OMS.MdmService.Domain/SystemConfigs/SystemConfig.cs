using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public partial class SystemConfig : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Description { get; set; }

        [NotNull]
        public virtual string Value { get; set; }

        [NotNull]
        public virtual string DefaultValue { get; set; }

        public virtual bool EditableByTenant { get; set; }

        public virtual ControlType ControlType { get; set; }

        [CanBeNull]
        public virtual string DataSource { get; set; }

        public SystemConfig()
        {

        }

        public SystemConfig(Guid id, string code, string description, string value, string defaultValue, bool editableByTenant, ControlType controlType, string dataSource)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), SystemConfigConsts.CodeMaxLength, SystemConfigConsts.CodeMinLength);
            Check.NotNull(description, nameof(description));
            Check.Length(description, nameof(description), SystemConfigConsts.DescriptionMaxLength, SystemConfigConsts.DescriptionMinLength);
            Check.NotNull(value, nameof(value));
            Check.Length(value, nameof(value), SystemConfigConsts.ValueMaxLength, SystemConfigConsts.ValueMinLength);
            Check.NotNull(defaultValue, nameof(defaultValue));
            Check.Length(defaultValue, nameof(defaultValue), SystemConfigConsts.DefaultValueMaxLength, SystemConfigConsts.DefaultValueMinLength);
            Check.Length(dataSource, nameof(dataSource), SystemConfigConsts.DataSourceMaxLength, 0);
            Code = code;
            Description = description;
            Value = value;
            DefaultValue = defaultValue;
            EditableByTenant = editableByTenant;
            ControlType = controlType;
            DataSource = dataSource;
        }

    }
}