using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual bool Active { get; set; }

        public SalesOrgHeader()
        {

        }

        public SalesOrgHeader(Guid id, string code, string name, bool active)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);
            Code = code;
            Name = name;
            Active = active;
        }

    }
}