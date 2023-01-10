using DMSpro.OMS.MdmService.ItemAttributes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValuesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemAttributesDataSeedContributor _itemAttributesDataSeedContributor;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor;

        public ItemAttributeValuesDataSeedContributor(IItemAttributeValueRepository itemAttributeValueRepository, IUnitOfWorkManager unitOfWorkManager, ItemAttributesDataSeedContributor itemAttributesDataSeedContributor, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor)
        {
            _itemAttributeValueRepository = itemAttributeValueRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemAttributesDataSeedContributor = itemAttributesDataSeedContributor; _itemAttributeValuesDataSeedContributor = itemAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemAttributesDataSeedContributor.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor.SeedAsync(context);

            await _itemAttributeValueRepository.InsertAsync(new ItemAttributeValue
            (
                id: Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"),
                attrValName: "7b5d35b967e44fc9a6e74c2ea8f6a2ff5655b58e63954c5e92ca3f02adfff6fa737de34a92c349359f17e4872cd7fdac32c3985d870d461fa9b66cd4d0a317c492d493280f6a4b1aaad745386c2b1cfe2428fa5f14584949af2b46dad3d5c9bc3520ca1e0ff345bfa5a21f93f006b929b63578c478224c5482ae8961323bb77",
                itemAttributeId: Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"),
                parentId: null
            ));

            await _itemAttributeValueRepository.InsertAsync(new ItemAttributeValue
            (
                id: Guid.Parse("c05ec9d8-99e4-444e-b228-c5273e917ec0"),
                attrValName: "c4dc8868fc8248e994fd438c298a74b4552e2c2c24cd48af81acfea85e4106ab431e38d2416749efb93818ad091a0d33b07e810519124c5c90c38af645dedfab2ff739fa8a2042d09c6c94c56024bdd9cb221540ce7a4034ad99d09bc0c51ffd540a1b68fbdd451582695f996a2ce057c3a7f3d123044b04a6ee0ef4c432ea9",
                itemAttributeId: Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"),
                parentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}