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
                id: Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"),
                dateVisit: new DateTime(2008, 3, 16),
                distance: 983691058,
                visitOrder: 512221060,
                dayOfWeek: default,
                week: 989657167,
                month: 1307483504,
                year: 558824439,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"),
                itemGroupId: null
            ));

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("473b81d2-fc78-4a5b-ba79-f56cb707d365"),
                dateVisit: new DateTime(2003, 2, 2),
                distance: 1043636306,
                visitOrder: 712029538,
                dayOfWeek: default,
                week: 612409450,
                month: 1532259848,
                year: 557855497,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"),
                itemGroupId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}