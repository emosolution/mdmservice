using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial interface IVisitPlanRepository : IRepository<VisitPlan, Guid>
    {
        Task<VisitPlanWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VisitPlanWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? mCPDetailId = null,
            Guid? customerId = null,
            Guid? routeId = null,
            Guid? companyId = null,
            Guid? itemGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VisitPlan>> GetListAsync(
                    string filterText = null,
                    DateTime? dateVisitMin = null,
                    DateTime? dateVisitMax = null,
                    int? distanceMin = null,
                    int? distanceMax = null,
                    int? visitOrderMin = null,
                    int? visitOrderMax = null,
                    DayOfWeek? dayOfWeek = null,
                    int? weekMin = null,
                    int? weekMax = null,
                    int? monthMin = null,
                    int? monthMax = null,
                    int? yearMin = null,
                    int? yearMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? dateVisitMin = null,
            DateTime? dateVisitMax = null,
            int? distanceMin = null,
            int? distanceMax = null,
            int? visitOrderMin = null,
            int? visitOrderMax = null,
            DayOfWeek? dayOfWeek = null,
            int? weekMin = null,
            int? weekMax = null,
            int? monthMin = null,
            int? monthMax = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? mCPDetailId = null,
            Guid? customerId = null,
            Guid? routeId = null,
            Guid? companyId = null,
            Guid? itemGroupId = null,
            CancellationToken cancellationToken = default);
    }
}