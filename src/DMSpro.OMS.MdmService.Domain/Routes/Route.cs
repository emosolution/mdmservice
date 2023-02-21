using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemGroups;
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

namespace DMSpro.OMS.MdmService.Routes
{
    public partial class Route : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool CheckIn { get; set; }

        public virtual bool CheckOut { get; set; }

        public virtual bool GPSLock { get; set; }

        public virtual bool OutRoute { get; set; }
        public Guid RouteTypeId { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        
        public Route()
        {

        }

        public Route(Guid id, Guid routeTypeId, Guid itemGroupId, Guid salesOrgHierarchyId, bool checkIn, bool checkOut, bool gpsLock, bool outRoute)
        {

            Id = id;
            CheckIn = checkIn;
            CheckOut = checkOut;
            GPSLock = gpsLock;
            OutRoute = outRoute;
            RouteTypeId = routeTypeId;
            ItemGroupId = itemGroupId;
            SalesOrgHierarchyId = salesOrgHierarchyId;
        }

    }
}