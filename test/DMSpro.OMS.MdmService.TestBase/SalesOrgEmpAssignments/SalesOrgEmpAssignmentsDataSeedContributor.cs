using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly EmployeeProfilesDataSeedContributor _employeeProfilesDataSeedContributor;

        public SalesOrgEmpAssignmentsDataSeedContributor(ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, EmployeeProfilesDataSeedContributor employeeProfilesDataSeedContributor)
        {
            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _employeeProfilesDataSeedContributor = employeeProfilesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _employeeProfilesDataSeedContributor.SeedAsync(context);

            await _salesOrgEmpAssignmentRepository.InsertAsync(new SalesOrgEmpAssignment
            (
                id: Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"),
                isBase: true,
                effectiveDate: new DateTime(2007, 11, 7),
                endDate: new DateTime(2004, 5, 3),
                hierarchyCode: "aeb6692dc66d472e891aeaec764a5cffeec838a3c26f40a18f5ef199f46bd182d02d47a7c3ff488ba15de89b73ef55966be5146d2fb64b3c83f33bbff5cbaf0b25e94bd44fb64d119658bb262bf60512677abbd2389e4a219c143403cd8f810c0ffa9358be264b3b9059a5bf041f20d9fa87dd517d1244ff9565e8fe4f151a935f634889cad4417bab390a76f039d459af0b626d1b114ed7b2ee75582a6af7adc2dc7998d2ee4afe9a3c870a376bdf2092da64caa6d34e099fd1c15f8e807dfa23c99b8791da4dbcb9b2c54ff39269b6cd07af4f372240d499ab3b931a47adc14acb1368477145da8a1011d4b72dc85ec000060340504fd2bc94",
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _salesOrgEmpAssignmentRepository.InsertAsync(new SalesOrgEmpAssignment
            (
                id: Guid.Parse("a1ca850d-9877-4814-a7aa-0b5814f5383a"),
                isBase: true,
                effectiveDate: new DateTime(2003, 2, 21),
                endDate: new DateTime(2014, 7, 23),
                hierarchyCode: "70c714d250e743f785febba04edd7cd1f10a74f8c525454389cf2b100fba0106cdbf3ef5a2a24c379482c996b5c74a2d7b0dc0d265aa45c5b60ac8583ec98310ae568f44f6434d158b3dba43f4489f51d48ffb68829c4eeb82727ce9bbc1020ec1920e249e844b89b5ac09cd664893457d04634d927b4a2287b7f8138bd8f994be15c40480ae4d9d8af6bd8c6b6f4497068a9a1925fc4409b604826c9e28f64f7193cf3b6880434899cf879bcb07d2cd44550110eded48a5a13e7514fd6bf18b582ff6ef17524eb4ae53397e6a1c00c893bc289496b84b61a263ec5e2f8eeb715a55172bf39e4133bc990a85fce3a1c8638de0325d8b4f34adb5",
                salesOrgHierarchyId: Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                employeeProfileId: Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}