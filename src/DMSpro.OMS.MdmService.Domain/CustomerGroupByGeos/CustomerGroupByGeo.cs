using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMasterId { get; set; }

        public CustomerGroupByGeo()
        {

        }

        public CustomerGroupByGeo(Guid id, Guid customerGroupId, Guid geoMasterId, bool active, DateTime? effectiveDate = null)
        {

            Id = id;
            Active = active;
            EffectiveDate = effectiveDate;
            CustomerGroupId = customerGroupId;
            GeoMasterId = geoMasterId;
        }

    }
}