using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public partial class DimensionMeasurement : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual uint Value { get; set; }

        public DimensionMeasurement()
        {

        }

        public DimensionMeasurement(Guid id, string code, string name, uint value)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), DimensionMeasurementConsts.CodeMaxLength, DimensionMeasurementConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), DimensionMeasurementConsts.NameMaxLength, DimensionMeasurementConsts.NameMinLength);
            Code = code;
            Name = name;
            Value = value;
        }

    }
}