using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.VATs;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        
        private readonly VATsDataSeedContributor _vATsDataSeedContributor;

        private readonly UOMGroupsDataSeedContributor _uOMGroupsDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor0;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor1;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor2;

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

        public ItemsDataSeedContributor(IItemRepository itemRepository, 
            IUnitOfWorkManager unitOfWorkManager,
            VATsDataSeedContributor vATsDataSeedContributor, 
            UOMGroupsDataSeedContributor uOMGroupsDataSeedContributor, 
            UOMsDataSeedContributor uOMsDataSeedContributor0,
            UOMsDataSeedContributor uOMsDataSeedContributor1, 
            UOMsDataSeedContributor uOMsDataSeedContributor2, 
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
            _vATsDataSeedContributor = vATsDataSeedContributor; 
            _uOMGroupsDataSeedContributor = uOMGroupsDataSeedContributor;
            _uOMsDataSeedContributor0 = uOMsDataSeedContributor0;
            _uOMsDataSeedContributor1 = uOMsDataSeedContributor1; 
            _uOMsDataSeedContributor2 = uOMsDataSeedContributor2;
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

            await _vATsDataSeedContributor.SeedAsync(context);
            await _uOMGroupsDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor0.SeedAsync(context);
            await _uOMsDataSeedContributor1.SeedAsync(context);
            await _uOMsDataSeedContributor2.SeedAsync(context);
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
                id: Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"),
                code: "c044c01babf1464d8e65",
                name: "cb3e0b3e45d0423f8b4787a38dff9480c6df0817887747cea8ca714bf069a25f3e0c2200c81f47258859c76ff06ad01d595fd14f8cc04efe85608ac0cbec266156fb2e211a8b4532bafe100a6d56ec7daf4ae04e173a43c4aa89321448fdfacb9e0de4ca79804e1384cb9608ccac01ba8703c69a36ff4fc882bebf58eda9190",
                shortName: "30f728ec43c045ec8247ffaa6a342e0e9afe77a7348645c4bdaf43cbbc78a96478b314e37c854bf6879e611f97fa5b3ea95a0f6660c744cabcbc239f7b8442511a95b7b0416d4962b1b23b34b50d924d83574f526f084261a5971b1c84e0320a739cb11e1c2a462daac57bda9665c55f1c8cedbf91b54769a24596873d9d0c8",
                erpCode: "945a90ef55354e0294cf",
                barcode: "daa5b45b12644e37889345387314f7a57b8622b22e344bf3ae",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 937374410,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 1531633523,
                issueMethod: default,
                canUpdate: true,
                purUnitRate: 1317906569,
                salesUnitRate: 1457712268,
                itemType: default,
                vatId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uomGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                purUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                salesUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
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
                id: Guid.Parse("75911d26-2787-40e1-9c0c-0fa58211b3da"),
                code: "ae34af1fff4f44b084b2",
                name: "907256313b724328b4c58ae1f553c5c151da08d3c4364589bfaf4641e17a50f01ecad6f1e61b4a09aac791e49c6e359feeb85d57bfe3477b94829c323b678b19cc3a441d9c8b4097bd70d7c53871c7c84a45221f88344017b65e747d6592a6da50cfc0b572784eb9a277abac63a8de930fca2546938a4af080db93354c05713",
                shortName: "a0dec9af01134b37a83c5a99773954a7a192b40eeb004f689035f22d51da1cfa8767bbc4897942b3ae83aed447999ba87760533dd5084cd7b2deaabdbda7f83ddcad8930bfbf4c24a94c2dbf080769c7c1d784d55db445dc8cf5aa3cebbe3864a7bbe65591624fb89a81f8d0137562752b28e7caba324f0b8511e75791de9e8",
                erpCode: "18dc58fbf1494c0c8703",
                barcode: "a0ac8d8596d84722b429f70b73639b2361cdef5e964c44a782",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 1862005778,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 1105758068,
                issueMethod: default,
                canUpdate: true,
                purUnitRate: 1449709802,
                salesUnitRate: 1244251629,
                itemType: default,
                vatId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uomGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                purUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                salesUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
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