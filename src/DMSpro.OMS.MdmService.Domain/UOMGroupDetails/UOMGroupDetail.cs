using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual uint AltQty { get; set; }

        public virtual uint BaseQty { get; set; }

        public virtual bool Active { get; set; }
        public Guid UOMGroupId { get; set; }
        public Guid AltUOMId { get; set; }
        public Guid BaseUOMId { get; set; }

        public UOMGroupDetail()
        {

        }

        public UOMGroupDetail(Guid id, Guid uOMGroupId, Guid altUOMId, Guid baseUOMId, uint altQty, uint baseQty, bool active)
        {

            Id = id;
            AltQty = altQty;
            BaseQty = baseQty;
            Active = active;
            UOMGroupId = uOMGroupId;
            AltUOMId = altUOMId;
            BaseUOMId = baseUOMId;
        }

    }
}