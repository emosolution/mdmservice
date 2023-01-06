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
                id: Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"),
                dateVisit: new DateTime(2014, 3, 14),
                distance: 59657487,
                visitOrder: 1144794777,
                dayOfWeek: default,
                week: 252133698,
                month: 1240510628,
                year: 1264736186,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _visitPlanRepository.InsertAsync(new VisitPlan
            (
                id: Guid.Parse("56b0c461-ef96-495e-8a92-2f7fc7779da8"),
                dateVisit: new DateTime(2012, 6, 17),
                distance: 1194760013,
                visitOrder: 2047879628,
                dayOfWeek: default,
                week: 1478265671,
                month: 1823876200,
                year: 1071534353,
                mCPDetailId: Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                customerId: Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                routeId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}