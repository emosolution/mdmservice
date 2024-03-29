using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial class SalesOrgHierarchy : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        public virtual int Level { get; set; }

        public virtual bool IsRoute { get; set; }

        public virtual bool IsSellingZone { get; set; }

        [NotNull]
        public virtual string HierarchyCode { get; set; }

        public virtual bool Active { get; set; }

        public virtual int DirectChildren { get; set; }
        public Guid SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }
        
        public SalesOrgHierarchy()
        {

        }

        public SalesOrgHierarchy(Guid id, Guid salesOrgHeaderId, Guid? parentId, string code, string name, int level, bool isRoute, bool isSellingZone, string hierarchyCode, bool active, int directChildren)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHierarchyConsts.CodeMaxLength, SalesOrgHierarchyConsts.CodeMinLength);
            Check.Length(name, nameof(name), SalesOrgHierarchyConsts.NameMaxLength, 0);
            if (level < SalesOrgHierarchyConsts.LevelMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(level), level, "The value of 'level' cannot be lower than " + SalesOrgHierarchyConsts.LevelMinLength);
            }

            if (level > SalesOrgHierarchyConsts.LevelMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(level), level, "The value of 'level' cannot be greater than " + SalesOrgHierarchyConsts.LevelMaxLength);
            }

            Check.NotNull(hierarchyCode, nameof(hierarchyCode));
            Check.Length(hierarchyCode, nameof(hierarchyCode), SalesOrgHierarchyConsts.HierarchyCodeMaxLength, SalesOrgHierarchyConsts.HierarchyCodeMinLength);
            Code = code;
            Name = name;
            Level = level;
            IsRoute = isRoute;
            IsSellingZone = isSellingZone;
            HierarchyCode = hierarchyCode;
            Active = active;
            DirectChildren = directChildren;
            SalesOrgHeaderId = salesOrgHeaderId;
            ParentId = parentId;
        }

    }
}