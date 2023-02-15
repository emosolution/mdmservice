using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public partial class MCPDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual int Distance { get; set; }

        public virtual int VisitOrder { get; set; }

        public virtual bool Monday { get; set; }

        public virtual bool Tuesday { get; set; }

        public virtual bool Wednesday { get; set; }

        public virtual bool Thursday { get; set; }

        public virtual bool Friday { get; set; }

        public virtual bool Saturday { get; set; }

        public virtual bool Sunday { get; set; }

        public virtual bool Week1 { get; set; }

        public virtual bool Week2 { get; set; }

        public virtual bool Week3 { get; set; }

        public virtual bool Week4 { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MCPHeaderId { get; set; }

        public MCPDetail()
        {

        }

        public MCPDetail(Guid id, Guid customerId, Guid mCPHeaderId, string code, DateTime effectiveDate, int distance, int visitOrder, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, bool week1, bool week2, bool week3, bool week4, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), MCPDetailConsts.CodeMaxLength, MCPDetailConsts.CodeMinLength);
            Code = code;
            EffectiveDate = effectiveDate;
            Distance = distance;
            VisitOrder = visitOrder;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
            Week1 = week1;
            Week2 = week2;
            Week3 = week3;
            Week4 = week4;
            EndDate = endDate;
            CustomerId = customerId;
            MCPHeaderId = mCPHeaderId;
        }

    }
}