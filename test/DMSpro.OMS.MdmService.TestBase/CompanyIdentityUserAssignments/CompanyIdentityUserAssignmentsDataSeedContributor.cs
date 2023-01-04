using DMSpro.OMS.MdmService.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        public CompanyIdentityUserAssignmentsDataSeedContributor(ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);

            await _companyIdentityUserAssignmentRepository.InsertAsync(new CompanyIdentityUserAssignment
            (
                id: Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"),
                identityUserId: Guid.Parse("c3ec831c-f0a5-4963-b161-3d1e0d33d458"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _companyIdentityUserAssignmentRepository.InsertAsync(new CompanyIdentityUserAssignment
            (
                id: Guid.Parse("855a993b-4001-46c7-b2b7-895923744d50"),
                identityUserId: Guid.Parse("270bd74a-2869-4f01-8810-3321b992288e"),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}