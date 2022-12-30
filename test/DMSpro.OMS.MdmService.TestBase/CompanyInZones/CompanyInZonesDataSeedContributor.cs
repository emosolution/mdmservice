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

        public CompanyInZonesDataSeedContributor(ICompanyInZoneRepository companyInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _companyInZoneRepository = companyInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"),
                effectiveDate: new DateTime(2012, 8, 2),
                endDate: new DateTime(2013, 3, 3),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("419472a0-78eb-4bae-b9af-ebc2c8122115"),
                effectiveDate: new DateTime(2001, 1, 4),
                endDate: new DateTime(2005, 1, 16),
                salesOrgHierarchyId: Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}