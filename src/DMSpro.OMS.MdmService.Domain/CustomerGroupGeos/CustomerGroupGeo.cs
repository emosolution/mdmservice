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

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        public virtual bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMaster0Id { get; set; }
        public Guid GeoMaster1Id { get; set; }
        public Guid GeoMaster2Id { get; set; }
        public Guid GeoMaster3Id { get; set; }
        public Guid GeoMaster4Id { get; set; }

        public CustomerGroupGeo()
        {

        }

        public CustomerGroupGeo(Guid id, Guid customerGroupId, Guid geoMaster0Id, Guid geoMaster1Id, Guid geoMaster2Id, Guid geoMaster3Id, Guid geoMaster4Id, string description, bool active)
        {

            Id = id;
            Check.Length(description, nameof(description), CustomerGroupGeoConsts.DescriptionMaxLength, 0);
            Description = description;
            Active = active;
            CustomerGroupId = customerGroupId;
            GeoMaster0Id = geoMaster0Id;
            GeoMaster1Id = geoMaster1Id;
            GeoMaster2Id = geoMaster2Id;
            GeoMaster3Id = geoMaster3Id;
            GeoMaster4Id = geoMaster4Id;
        }

    }
}