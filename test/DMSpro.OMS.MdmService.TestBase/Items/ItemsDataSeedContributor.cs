using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        private readonly VATsDataSeedContributor _vATsDataSeedContributor;

        private readonly UOMGroupsDataSeedContributor _uOMGroupsDataSeedContributor;

        private readonly UOMGroupDetailsDataSeedContributor _uOMGroupDetailsDataSeedContributor0;

        private readonly UOMGroupDetailsDataSeedContributor _uOMGroupDetailsDataSeedContributor1;

        private readonly UOMGroupDetailsDataSeedContributor _uOMGroupDetailsDataSeedContributor2;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor0;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor1;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor2;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor3;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor4;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor5;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor6;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor7;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor8;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor9;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor10;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor11;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor12;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor13;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor14;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor15;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor16;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor17;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor18;

        private readonly ItemAttributeValuesDataSeedContributor _itemAttributeValuesDataSeedContributor19;

        public ItemsDataSeedContributor(IItemRepository itemRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor, VATsDataSeedContributor vATsDataSeedContributor, 
            UOMGroupsDataSeedContributor uOMGroupsDataSeedContributor, 
            UOMGroupDetailsDataSeedContributor uOMGroupDetailsDataSeedContributor0,
            UOMGroupDetailsDataSeedContributor uOMGroupDetailsDataSeedContributor1, 
            UOMGroupDetailsDataSeedContributor uOMGroupDetailsDataSeedContributor2, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor0, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor1,
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor2, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor3,
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor4, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor5,
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor6, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor7, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor8, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor9, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor10, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor11, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor12, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor13, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor14, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor15, 
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor16, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor17,
            ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor18, ItemAttributeValuesDataSeedContributor itemAttributeValuesDataSeedContributor19)
        {
            _itemRepository = itemRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _vATsDataSeedContributor = vATsDataSeedContributor; 
            _uOMGroupsDataSeedContributor = uOMGroupsDataSeedContributor;
            _uOMGroupDetailsDataSeedContributor0 = uOMGroupDetailsDataSeedContributor0;
            _uOMGroupDetailsDataSeedContributor1 = uOMGroupDetailsDataSeedContributor1; 
            _uOMGroupDetailsDataSeedContributor2 = uOMGroupDetailsDataSeedContributor2;
            _itemAttributeValuesDataSeedContributor0 = itemAttributeValuesDataSeedContributor0; _itemAttributeValuesDataSeedContributor1 = itemAttributeValuesDataSeedContributor1; 
            _itemAttributeValuesDataSeedContributor2 = itemAttributeValuesDataSeedContributor2; _itemAttributeValuesDataSeedContributor3 = itemAttributeValuesDataSeedContributor3;
            _itemAttributeValuesDataSeedContributor4 = itemAttributeValuesDataSeedContributor4; _itemAttributeValuesDataSeedContributor5 = itemAttributeValuesDataSeedContributor5;
            _itemAttributeValuesDataSeedContributor6 = itemAttributeValuesDataSeedContributor6; _itemAttributeValuesDataSeedContributor7 = itemAttributeValuesDataSeedContributor7;
            _itemAttributeValuesDataSeedContributor8 = itemAttributeValuesDataSeedContributor8; _itemAttributeValuesDataSeedContributor9 = itemAttributeValuesDataSeedContributor9;
            _itemAttributeValuesDataSeedContributor10 = itemAttributeValuesDataSeedContributor10; _itemAttributeValuesDataSeedContributor11 = itemAttributeValuesDataSeedContributor11; 
            _itemAttributeValuesDataSeedContributor12 = itemAttributeValuesDataSeedContributor12; _itemAttributeValuesDataSeedContributor13 = itemAttributeValuesDataSeedContributor13; 
            _itemAttributeValuesDataSeedContributor14 = itemAttributeValuesDataSeedContributor14; _itemAttributeValuesDataSeedContributor15 = itemAttributeValuesDataSeedContributor15; 
            _itemAttributeValuesDataSeedContributor16 = itemAttributeValuesDataSeedContributor16; _itemAttributeValuesDataSeedContributor17 = itemAttributeValuesDataSeedContributor17; 
            _itemAttributeValuesDataSeedContributor18 = itemAttributeValuesDataSeedContributor18; _itemAttributeValuesDataSeedContributor19 = itemAttributeValuesDataSeedContributor19;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);
            await _vATsDataSeedContributor.SeedAsync(context);
            await _uOMGroupsDataSeedContributor.SeedAsync(context);
            await _uOMGroupDetailsDataSeedContributor0.SeedAsync(context);
            await _uOMGroupDetailsDataSeedContributor1.SeedAsync(context);
            await _uOMGroupDetailsDataSeedContributor2.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor0.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor1.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor2.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor3.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor4.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor5.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor6.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor7.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor8.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor9.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor10.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor11.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor12.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor13.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor14.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor15.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor16.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor17.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor18.SeedAsync(context);
            await _itemAttributeValuesDataSeedContributor19.SeedAsync(context);

            await _itemRepository.InsertAsync(new Item
            (
                id: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"),
                code: "1976a166ea4846228637",
                name: "65e2326954044b5e82715b2cb96387c50a889e2c42164747b4f8a65ec9e21e471240683096184168830482adda2c76212332466ba7844fe1b92b1df7f7d4ebaf15cdc6426ca547569cbc8e4fe14a682cb05981d6bb73495a9d5582adb0514f0c90f8cb6206d1444fbdf47c67d947ef0ea1b30bb4512f4fe793a85962b299e6e",
                shortName: "34a35ed4bba44b4aa9c0eafccc3e715c91339f474c664279bc22ce976fc46cf205ba4fef8316478d9ef7fcdb5c3eea4bfcd5fda0d53242418d486f7dd6268f819fb6bc449e6d4b79b470c4d081d8f1a6a25bc7f623bf4a799b6ef36873ca51caa69c5d109e644e199279b82ff9a343ed633b1dd4b2534009be6418f7533fa4d",
                eRPCode: "1ea8d46dacb441468efd",
                barcode: "8d906e562f0c42c28899e349f4b003951943a61229d64c5b9f",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 2084867762,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 2044657824,
                issueMethod: default,
                canUpdate: true,
                itemTypeId: Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                vatId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uomGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                purUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                salesUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _itemRepository.InsertAsync(new Item
            (
                id: Guid.Parse("97499669-1c9a-41d3-b607-5e14e66c687c"),
                code: "3951c184e472413fbf6e",
                name: "9408fb7368404d319cef1f6ae081f208b8eb623d9ee5420da3712ef84d5def0d6b92d31f750141788e46848489c9004514a8878d3e0a4c03a0a80e60efe601296008f3cd527644f695cecd20cb2c201c87aa9c3ae1f8484fac430a39224cf806fc7cd95bc90b42869e98d48d178b5b06fd62f44c32894a92aa5e8daedc634ce",
                shortName: "928a17680baf4f3088a0ccb1da4d3e152b945a05c2ba4ccca320b4648a2ca5a0a7075976176f4d228acd9e0863046b873d8beb293874405592e1bc346ed8afbd0000683de87146ba8c0716dac785c9416f6892b5182d4c89b66b858097dd7e2276dde476594843a1b77fa0ae221e847737f012efd79d4e34adc4b2cc340f70a",
                eRPCode: "30236ccf626f43fa9140",
                barcode: "51f665011cbb49fbb28571654985104032b16f4732464aec89",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 908246605,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 956344089,
                issueMethod: default,
                canUpdate: true,
                itemTypeId: Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                vatId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uomGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                purUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                salesUOMId: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}