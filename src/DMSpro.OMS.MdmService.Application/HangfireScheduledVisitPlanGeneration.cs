using DMSpro.OMS.MdmService.VisitPlans;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService
{
    public interface IHangfireScheduledVisitPlanGeneration : IHangfireBackgroundWorker
    {
    }

    [ExposeServices(typeof(IHangfireScheduledVisitPlanGeneration))]
    public class HangfireScheduledVisitPlanGeneration : HangfireBackgroundWorkerBase, IHangfireScheduledVisitPlanGeneration
    {
        private static IVisitPlansScheduledAppService _visitPlanScheduledAppService;

        public HangfireScheduledVisitPlanGeneration(IVisitPlansScheduledAppService visitPlanScheduledAppService)
        {
            _visitPlanScheduledAppService = visitPlanScheduledAppService; 

            RecurringJobId = nameof(HangfireScheduledVisitPlanGeneration);
            CronExpression = "0 1 * * *"; // 1AM of everyday
        }

        [UnitOfWork]
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;
            var lastDayOfMonth = DateTime.DaysInMonth(now.Year, now.Month);
            if (now.Day != lastDayOfMonth) // skip if this is not the last day of the a month
            {
                Console.WriteLine("HangfireScheduledVisitPlanGeneration: Not the last day of month. Visit plan generation skipped.");
                return;
            }
            Console.WriteLine("HangfireScheduledVisitPlanGeneration: Last day of month detected. Begin to generate visit plan for all eligible MCPHeaders...");
            await _visitPlanScheduledAppService.SheduledGenerationAsync();
            Console.WriteLine("HangfireScheduledVisitPlanGeneration: Visit plans for all eligible MCPHeaders are generated successfully.");
        }
    }
}
