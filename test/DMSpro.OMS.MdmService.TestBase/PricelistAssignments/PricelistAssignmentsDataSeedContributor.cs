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
                id: Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"),
                description: "c57f206ced1940c29558d46f611d37e7cb5ba272a9864e988988af532212c324b2e4c1a2c27e422785b811bd9302e3bb3ea98e037a0f401abbdc56904a9763ce00f13f1a5e084d13a2af9b887600b4a731b2f9005e7b445c97d79d9f3c4d5730262c92c3481f4a06be2cebceeb47a47a6178a71ad72d438c99a8b158bd4d51efd1101cece682401d9592aef0b629b0db1929c903cba04103b66866f80c8d492532ff2dfeaa5d4c7fbf535d53be606b89fc211ae2a1b1426b89422301e69d9a8c3c94fa2133e44ca5bc776c9c95aab6706c31431c0dee479193f3649f35a3c7fc96379e308c4d4f6bb06ed292d3d2588ac7ede12b9d714f0b9d42",
                priceListId: Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("43280865-cc3b-4d46-b78b-29ebc68e7cbf"),
                description: "357145810c36486fb92c916649825ae5c78ac9d212484ba2aad885e50e78bdab22e610c6d1174dc5bc548b8d7aa4ee497c45958f98ba4564a5fa925d48125699a635f9d7150c47dbab5d6f09ec6a3fd99bfe0efbf5f247e3b2c920ed5ba086906cbba3c700504077b6c62b643707975bdd26286385c4499f8d29c6a01889724009bc75260e3e4aa69dba9f6118e673acc2f9998ef86e4aa68fbfe1ff3066c531df0fc786f05b41ea94f3da1efb39515e19f8ee2629884996891a838e9029cb7c5ea4aacfdbc747af930bc15572b68b4f246882248e044ddabe951feafa869630fe0400e324494a75826962bdfa7d2878c781769faf224b4b91a8",
                priceListId: Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}