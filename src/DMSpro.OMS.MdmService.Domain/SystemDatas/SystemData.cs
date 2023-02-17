using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial class SystemData : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string ValueCode { get; set; }

        [NotNull]
        public virtual string ValueName { get; set; }

        public SystemData()
        {

        }

        public SystemData(Guid id, string code, string valueCode, string valueName)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), SystemDataConsts.CodeMaxLength, SystemDataConsts.CodeMinLength);
            Check.NotNull(valueCode, nameof(valueCode));
            Check.Length(valueCode, nameof(valueCode), SystemDataConsts.ValueCodeMaxLength, SystemDataConsts.ValueCodeMinLength);
            Check.NotNull(valueName, nameof(valueName));
            Check.Length(valueName, nameof(valueName), SystemDataConsts.ValueNameMaxLength, SystemDataConsts.ValueNameMinLength);
            Code = code;
            ValueCode = valueCode;
            ValueName = valueName;
        }

    }
}