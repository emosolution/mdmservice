using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CompanyInZones;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZonesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        public CompanyInZonesDataSeedContributor(ICompanyInZoneRepository companyInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor)
        {
            _companyInZoneRepository = companyInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor; _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);
            await _itemGroupsDataSeedContributor.SeedAsync(context);

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"),
                effectiveDate: new DateTime(2015, 1, 5),
                endDate: new DateTime(2010, 8, 21),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("41e56b56-64eb-4e0c-ac8c-2462d6aa3b42"),
                effectiveDate: new DateTime(2007, 11, 1),
                endDate: new DateTime(2020, 8, 4),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}