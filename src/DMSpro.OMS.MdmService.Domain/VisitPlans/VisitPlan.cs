using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

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

        public virtual bool IsCommando { get; set; }

        public Guid MCPDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RouteId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public VisitPlan()
        {

        }

        public VisitPlan(Guid id, Guid mCPDetailId, Guid customerId, Guid routeId, Guid? itemGroupId, DateTime dateVisit, int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year, bool isCommando)
        {

            Id = id;
            DateVisit = dateVisit;
            Distance = distance;
            VisitOrder = visitOrder;
            DayOfWeek = dayOfWeek;
            Week = week;
            Month = month;
            Year = year;
            IsCommando = isCommando;
            MCPDetailId = mCPDetailId;
            CustomerId = customerId;
            RouteId = routeId;
            ItemGroupId = itemGroupId;
        }
    }
}