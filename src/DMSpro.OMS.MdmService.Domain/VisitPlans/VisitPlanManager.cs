using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanManager : DomainService
    {
        private readonly IVisitPlanRepository _visitPlanRepository;

        public VisitPlanManager(IVisitPlanRepository visitPlanRepository)
        {
            _visitPlanRepository = visitPlanRepository;
        }

        public async Task<VisitPlan> CreateAsync(
            Guid mCPDetailId, Guid customerId, Guid routeId, Guid? itemGroupId, DateTime dateVisit, 
            int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year, 
            bool isCommando)
        {
            Check.NotNull(mCPDetailId, nameof(mCPDetailId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(dateVisit, nameof(dateVisit));
            Check.NotNull(dayOfWeek, nameof(dayOfWeek));

            var visitPlan = new VisitPlan(
                 GuidGenerator.Create(),
                 mCPDetailId, customerId, routeId, itemGroupId, 
                 dateVisit, distance, visitOrder, dayOfWeek, week, month, year, 
                 isCommando);

            return await _visitPlanRepository.InsertAsync(visitPlan);
        }

        public async Task<VisitPlan> UpdateAsync(
            Guid id,
            Guid mCPDetailId, Guid customerId, Guid routeId, Guid? itemGroupId, 
            DateTime dateVisit, int distance, int visitOrder, 
            DayOfWeek dayOfWeek, int week, int month, int year, 
            bool isCommando, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(mCPDetailId, nameof(mCPDetailId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(dateVisit, nameof(dateVisit));
            Check.NotNull(dayOfWeek, nameof(dayOfWeek));

            var visitPlan = await _visitPlanRepository.GetAsync(id);

            visitPlan.MCPDetailId = mCPDetailId;
            visitPlan.CustomerId = customerId;
            visitPlan.RouteId = routeId;
            visitPlan.ItemGroupId = itemGroupId;
            visitPlan.DateVisit = dateVisit;
            visitPlan.Distance = distance;
            visitPlan.VisitOrder = visitOrder;
            visitPlan.DayOfWeek = dayOfWeek;
            visitPlan.Week = week;
            visitPlan.Month = month;
            visitPlan.Year = year;
            visitPlan.IsCommando = isCommando;

            visitPlan.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _visitPlanRepository.UpdateAsync(visitPlan);
        }

    }
}