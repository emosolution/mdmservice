using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public partial class MCPHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
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

        public virtual SalesOrgHierarchy SalesOrgHierarchy { get; set; }
        public virtual Company Company { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

        public MCPHeader()
        {

        }

        public MCPHeader(Guid id, Guid routeId, Guid companyId, Guid? itemGroupId, string code, string name, DateTime effectiveDate, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Check.Length(name, nameof(name), MCPHeaderConsts.NameMaxLength, 0);
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