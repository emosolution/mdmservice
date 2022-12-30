using DMSpro.OMS.MdmService.MCPDetails;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.VisitPlans;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlansDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly MCPDetailsDataSeedContributor _mCPDetailsDataSeedContributor;

        public VisitPlansDataSeedContributor(IVisitPlanRepository visitPlanRepository, IUnitOfWorkManager unitOfWorkManager, MCPDetailsDataSeedContributor mCPDetailsDataSeedContributor)
        {
            _visitPlanRepository = visitPlanRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _mCPDetailsDataSeedContributor = mCPDetailsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _mCPDetailsDataSeedContributor.SeedAsync(context);

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"),
                dateVisit: new DateTime(2009, 4, 2),
                distance: 1490800628,
                visitOrder: 2129893834,
                dayOfWeek: default,
                week: 797932690,
                month: 1281407949,
                year: 1458397198,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9")
            ));

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("fd5dcdb3-5c17-4d42-97b0-af0437e6b5f0"),
                dateVisit: new DateTime(2012, 3, 22),
                distance: 305683410,
                visitOrder: 413658954,
                dayOfWeek: default,
                week: 1379512652,
                month: 1770738438,
                year: 749933573,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}