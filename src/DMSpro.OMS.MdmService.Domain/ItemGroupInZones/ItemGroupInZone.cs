using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial class ItemGroupInZone : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual bool Active { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }
        public Guid SellingZoneId { get; set; }
        public Guid ItemGroupId { get; set; }

        public ItemGroupInZone()
        {

        }

        public ItemGroupInZone(Guid id, Guid sellingZoneId, Guid itemGroupId, DateTime effectiveDate, bool active, string description, DateTime? endDate = null)
        {

            Id = id;
            Check.Length(description, nameof(description), ItemGroupInZoneConsts.DescriptionMaxLength, 0);
            EffectiveDate = effectiveDate;
            Active = active;
            Description = description;
            EndDate = endDate;
            SellingZoneId = sellingZoneId;
            ItemGroupId = itemGroupId;
        }

    }
}