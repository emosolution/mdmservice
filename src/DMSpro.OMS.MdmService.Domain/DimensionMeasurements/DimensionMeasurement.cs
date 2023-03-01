using System;
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

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual decimal Value { get; set; }

        public DimensionMeasurement()
        {

        }

        public DimensionMeasurement(Guid id, string code, string name, decimal value)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), DimensionMeasurementConsts.CodeMaxLength, DimensionMeasurementConsts.CodeMinLength);
            Check.Length(name, nameof(name), DimensionMeasurementConsts.NameMaxLength, 0);
            Code = code;
            Name = name;
            Value = value;
        }

    }
}