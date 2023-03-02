using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public partial class WeightMeasurement : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual decimal Value { get; set; }

        public WeightMeasurement()
        {

        }

        public WeightMeasurement(Guid id, string code, string name, decimal value)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(name, nameof(name), WeightMeasurementConsts.NameMaxLength, 0);
            Code = code;
            Name = name;
            Value = value;
        }

    }
}