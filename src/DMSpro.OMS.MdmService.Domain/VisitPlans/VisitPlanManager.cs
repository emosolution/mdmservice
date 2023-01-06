using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        Guid mCPDetailId, Guid customerId, Guid routeId, Guid companyId, Guid itemGroupId, DateTime dateVisit, int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year)
        {
            Check.NotNull(mCPDetailId, nameof(mCPDetailId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(dateVisit, nameof(dateVisit));
            Check.NotNull(dayOfWeek, nameof(dayOfWeek));

            var visitPlan = new VisitPlan(
             GuidGenerator.Create(),
             mCPDetailId, customerId, routeId, companyId, itemGroupId, dateVisit, distance, visitOrder, dayOfWeek, week, month, year
             );

            return await _visitPlanRepository.InsertAsync(visitPlan);
        }

        public async Task<VisitPlan> UpdateAsync(
            Guid id,
            Guid mCPDetailId, Guid customerId, Guid routeId, Guid companyId, Guid itemGroupId, DateTime dateVisit, int distance, int visitOrder, DayOfWeek dayOfWeek, int week, int month, int year, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(mCPDetailId, nameof(mCPDetailId));
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(dateVisit, nameof(dateVisit));
            Check.NotNull(dayOfWeek, nameof(dayOfWeek));

            var queryable = await _visitPlanRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var visitPlan = await AsyncExecuter.FirstOrDefaultAsync(query);

            visitPlan.MCPDetailId = mCPDetailId;
            visitPlan.CustomerId = customerId;
            visitPlan.RouteId = routeId;
            visitPlan.CompanyId = companyId;
            visitPlan.ItemGroupId = itemGroupId;
            visitPlan.DateVisit = dateVisit;
            visitPlan.Distance = distance;
            visitPlan.VisitOrder = visitOrder;
            visitPlan.DayOfWeek = dayOfWeek;
            visitPlan.Week = week;
            visitPlan.Month = month;
            visitPlan.Year = year;

            visitPlan.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _visitPlanRepository.UpdateAsync(visitPlan);
        }

    }
}