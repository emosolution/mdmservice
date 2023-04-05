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
                id: Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"),
                attrValName: "d917f19ecb31403689a02f66c7e6502b72018666603441efadd5ef404b6755f06caa1ce9fec34479b6a9cfe79757ba525fd01f3636f74429ad41bbd920583efba76a0def261441fa811ef188d1d7360e8a95bb545f0b45eda77ebf8c3c985ba92f9ea600ec054e2782418a7cab0a900fa61dc924814a44349c46805948adb47",
                code: "asdhio33nl4tasfijapsfjas",
                itemAttributeId: Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"),
                parentId: null
            ));

            await _itemAttributeValueRepository.InsertAsync(new ItemAttributeValue
            (
                id: Guid.Parse("e038b80d-7a00-467b-9366-12409f4a2370"),
                attrValName: "0980af417f8e4cdf88b1415158e344e009e3b8a20f7046b1859103adcfa085ed028a7dff77624d0cb273b35ed5789c8e0df02982fe434a7bb6bbdfe50ce1c3e1dce1f9bf05134b9abf87ace886d998fab501ab092e684f8aa14462fd20126acfb273acf761834037a7225d40f43a94955781417f290e4b34be34a62b45b9c8d",
                itemAttributeId: Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"),
                code: "sjdfopkdsjfpdsjpfjdffsddf",
                parentId: null
            )); ;

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}