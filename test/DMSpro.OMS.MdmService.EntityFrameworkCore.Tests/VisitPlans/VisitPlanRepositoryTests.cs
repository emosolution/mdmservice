using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IVisitPlanRepository _visitPlanRepository;

        public VisitPlanRepositoryTests()
        {
            _visitPlanRepository = GetRequiredService<IVisitPlanRepository>();
        }
    }
}