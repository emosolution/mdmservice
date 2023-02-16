using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }
        public Guid RouteId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public MCPHeader()
        {

        }

        public MCPHeader(Guid id, Guid routeId, Guid companyId, Guid? itemGroupId, string code, string name, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Code = code;
            Name = name;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            RouteId = routeId;
            CompanyId = companyId;
            ItemGroupId = itemGroupId;
        }

    }
}