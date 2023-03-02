using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.PricelistAssignments;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        public PricelistAssignmentsDataSeedContributor(IPricelistAssignmentRepository pricelistAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor)
        {
            _pricelistAssignmentRepository = pricelistAssignmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _customerGroupsDataSeedContributor.SeedAsync(context);

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("2b969661-f3b7-439d-a59d-7645f2568324"),
                description: "32de5302b1864dbda8be541316f5e0b3fc09023628414e15a181e92a271739bae4e023eac7b44badbe751b170a0e6536233a7328cd63482c9d40360ba1e7ff8376eacec67d804f5cabc26a00086329f04244bb2a418048f18e05f81770038b24dd316d2b3fb84f86acf81a58e7f5ed73e4416837311d4107a92721266a89b23310bd9f0a67b1463182efa5ba2d8810077cb38e16699f4922968e9ecebfa1e783e6027f42bd164f59ac488fe2be6a00e038c8a118eb934aab9081726d4b3d05a1bdef6f6ffed244eaac67c88cb09c413711ca0e22cd8e41418cd94526bdb464839e09ea9031a84cf0ae762eae8ff440f37530d25a4c384c039ea1",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("696527d2-cbbb-4bae-b694-6dce484e2ade"),
                description: "494d4e99537a4c1ab8f67c672a03e6b7f9ef97fd9ac54952b9bd84150f1046c2eef1bd930e954166a0c7b7d45cda23dd4c4f778683ab414cb0de2638ebe271a99d0bf160442a4ee1817dac61d9aa4ebb5f34510d6c4746d6bdc288adb7f3d8c3f278f32999d84853967ef6d17666e837333f3bebbe104720ae3667443e742f77a7e84dc0bf814a93ba82338a1ce840c2d32416f4e4b7496cb804933f2af6662a3287019d30734920a64f55f9d0c69999841dd1df3c564f2586a7b160c4eae201b8dc39fa637c4b57862d6dc7f9d75401dd4a6172f90d4556ad3ec8c149ed1d0162b6cb6cdf594097a933b77b814c1f16825e2da7dbee46489f2a",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}