using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
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

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial class VisitPlan : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime DateVisit { get; set; }

        public virtual int Distance { get; set; }

        public virtual int VisitOrder { get; set; }

        public virtual DayOfWeek DayOfWeek { get; set; }

        public virtual int Week { get; set; }

        public virtual int Month { get; set; }

        public virtual int Year { get; set; }
        public Guid MCPDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RouteId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public virtual MCPDetail MCPDetail { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual SalesOrgHierarchy Route { get; set; }
        public virtual Company Company { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

        public VisitPlan()
        {

        }

        public VisitPlan(Guid id, Guid mCPDetailId, Guid customerId, Guid routeId, Guid companyId, Guid? itemGroupId, DateTime dateVisit, int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year)
        {

            Id = id;
            DateVisit = dateVisit;
            Distance = distance;
            VisitOrder = visitOrder;
            DayOfWeek = dayOfWeek;
            Week = week;
            Month = month;
            Year = year;
            MCPDetailId = mCPDetailId;
            CustomerId = customerId;
            RouteId = routeId;
            CompanyId = companyId;
            ItemGroupId = itemGroupId;
        }

    }
}