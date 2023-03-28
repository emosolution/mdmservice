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
                id: Guid.Parse("db548c16-3cf0-49b8-a724-8b8dfa26ffda"),
                description: "c9ebe18b18c441a3b1ef4c868fac4622b3c0687862cf4cf88e5893065bf9a711106899eb226044e1847bab8edc3b6d61e595a86c3e434ef69cedcfd212af43b61874044cd68c4ef69b3729ab6d713e64bc4da30662c14661ad703f0365b951ef631b1e7e3c3443dfb06b2aec083b23b55994f256ea3a47b2b547020156f57b887023a345af204539bb9a6111705020fdc0af32d81c594e94b2742551b001523e803906ad6ffa49d9a8397f842595419cd1ca403c60744085914b83b3784950eb397be5fe43904c1cb29de840bcd4297271e06830b44541e8bd827914ac0180f1e360259fc17e4f0aa80ff04aa39650a1bc09cb9f45e84ed781b0",
                releaseDate: new DateTime(2004, 3, 10),
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("e7a00afb-2350-4a64-9eae-2b9da33e247c"),
                description: "8d577df2d53c4e8393cba2d610e9a0dbff182f3e064f44a8b2951250d0d156b5495fd882b2ec4630a3fe6c91bd3aae556c815b431033480699257aed033fdb979976efa5dc094316bc89f0b5cfb9dfad1fe514ee08884653a0cb797f37b9def7e9b8d34a1f0c45149ffb0b6d57e3f98c4449a1ba65fa4d81b2ddb7cf93c5daab0ed5eafd67ba434096c543c21f352a899a68fad1b5fa49c5b8dbb0542605d07eef4f41256ee2461d998d9b330748ff9d40b881ca5c0b48b683a4e9ca0ba39857c898373eb2fa4f36a975e38939f25819751e361b3754452782496e81c57bc232e8e3a9495d234b0895b88ad3c666e8b04c4527f9b4cc430db571",
                releaseDate: new DateTime(2005, 1, 12),
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}