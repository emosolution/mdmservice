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
                id: Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"),
                effectiveDate: new DateTime(2008, 8, 25),
                endDate: new DateTime(2016, 5, 10),
                isBase: true,
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _companyInZoneRepository.InsertAsync(new CompanyInZone
            (
                id: Guid.Parse("5d00f7ed-c7a2-414d-9aaf-a0e9a518cedd"),
                effectiveDate: new DateTime(2010, 2, 4),
                endDate: new DateTime(2003, 1, 24),
                isBase: true,
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}