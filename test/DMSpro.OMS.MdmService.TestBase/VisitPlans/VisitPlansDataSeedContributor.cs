using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Customers;
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

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        public VisitPlansDataSeedContributor(IVisitPlanRepository visitPlanRepository, IUnitOfWorkManager unitOfWorkManager, MCPDetailsDataSeedContributor mCPDetailsDataSeedContributor, CustomersDataSeedContributor customersDataSeedContributor, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor)
        {
            _visitPlanRepository = visitPlanRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _mCPDetailsDataSeedContributor = mCPDetailsDataSeedContributor; _customersDataSeedContributor = customersDataSeedContributor; _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor; _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _mCPDetailsDataSeedContributor.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);
            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);
            await _itemGroupsDataSeedContributor.SeedAsync(context);

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"),
                dateVisit: new DateTime(2001, 3, 15),
                distance: 976386175,
                visitOrder: 495876567,
                dayOfWeek: default,
                week: 1788057009,
                month: 606316094,
                year: 913634963,
                isCommando: true,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                routeId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                itemGroupId: null
            ));

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("6653549e-0dde-4618-981f-0591c3e4b624"),
                dateVisit: new DateTime(2000, 11, 7),
                distance: 1359180350,
                visitOrder: 132117602,
                dayOfWeek: default,
                week: 43855291,
                month: 870617472,
                year: 907038626,
                isCommando: true,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                routeId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                itemGroupId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}