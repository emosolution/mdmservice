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
                id: Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"),
                effectiveDate: new DateTime(2003, 6, 9),
                endDate: new DateTime(2011, 1, 17),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: null
            ));

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("03f3d0f3-813b-4ca6-827e-bb2962d1edda"),
                effectiveDate: new DateTime(2009, 8, 2),
                endDate: new DateTime(2021, 9, 15),
                salesOrgHierarchyId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                itemGroupId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}