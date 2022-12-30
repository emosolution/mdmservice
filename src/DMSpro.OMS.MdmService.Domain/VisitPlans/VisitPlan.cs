using DMSpro.OMS.MdmService.MCPDetails;
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
    public class VisitPlan : FullAuditedAggregateRoot<Guid>, IMultiTenant
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

        public VisitPlan()
        {

        }

        public VisitPlan(Guid id, Guid mCPDetailId, DateTime dateVisit, int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year)
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
        }

    }
}