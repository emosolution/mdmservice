using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public partial class CustomerGroupByGeo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }

        public CustomerGroupByGeo()
        {

        }

        public CustomerGroupByGeo(Guid id, Guid customerGroupId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, bool active, DateTime? effectiveDate = null)
        {

            Id = id;
            Active = active;
            EffectiveDate = effectiveDate;
            CustomerGroupId = customerGroupId;
            GeoMaster0Id = geoMaster0Id;
            GeoMaster1Id = geoMaster1Id;
            GeoMaster2Id = geoMaster2Id;
            GeoMaster3Id = geoMaster3Id;
            GeoMaster4Id = geoMaster4Id;
        }

    }
}