using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.SystemDatas;
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
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

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

        public ItemsDataSeedContributor(IItemRepository itemRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor, VATsDataSeedContributor vATsDataSeedContributor, 
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
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _vATsDataSeedContributor = vATsDataSeedContributor; 
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

            await _systemDatasDataSeedContributor.SeedAsync(context);
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
                id: Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"),
                code: "0fb2857d1ace436e8d44",
                name: "0c987874b116471bbd08d8cd316a4b4c355b5ee0b9bd4331bfaf3bba86c1907bf7d812f109de446bb30a97ac77d871469895c4ca2e904fbf8cab40aa0cb8253b99adf9fa2e8c4c8a98b77f9d61d12b97491d4a4f7e1e4e828ae8c9afd93049b22cee5663f5014dcc8fee3914aafa2767c520ebb20e6343459495af25af067c9",
                shortName: "9bcac9b7363a473e96af98897b367f13b11022e68d2a48958b8a52dd959e3ad8ca6f95cded8c4c2383fa4706dc4defcf314c51275ad04426bc8ae1c2a4ca6263fec793f5daf14caa863772bb0228a2ec27f8d99c5d86446eb383806a3c13e3527bd54f743810464280b955f7a088ecc502b3141cce4548b89b3135beaeb02f7",
                erpCode: "d42de760d4184242b684",
                barcode: "18a9ac33b5a3407fa1934260ef3f6b26bae9e67b174546a6ae",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 1368830664,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 1108883893,
                issueMethod: default,
                canUpdate: true,
                purUnitRate: 2142153009,
                salesUnitRate: 529866238,
                itemTypeId: Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
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
                id: Guid.Parse("ca66e65e-0340-47a1-a505-9351422ca037"),
                code: "d13b0ba482c64ef6ae9e",
                name: "0fbd46d477f844749ed9b5180165c37781619646554c438e9bf32d18ee51800604117ff27b4c446fbe9025ddeed5003624c88bb05fe54dd9b812bf987f6fe4a93939ea411b8542ae9b0b0ba4106f0498797c3070c0b344d9b260dc6a525cf375c7598da75d2e4b2baa0cd0bccaa5cb7d79991f96271b41db9925ce9481ea44e",
                shortName: "f1b9c2f5ea474e898bcdaa9d42d0f6b3a0862b74e5874a19977c04e9c4107e6e401f9cc39d18434bab57744698391e46cc123fa63eea4b25a35e4ed6a42ea7e6d13e622170ea43b69ed334e977b8513b44764c905b2643068e60b55ebbe59d4f1f4a9f8d04d1498a8ca8d44f3bac478325d2eaa56e914ba68e1b2115302e424",
                erpCode: "e926997da7b3417b9f27",
                barcode: "bd7e23dd8f834673876fcfe0da21d982cd3033f66b8e41e6a5",
                isPurchasable: true,
                isSaleable: true,
                isInventoriable: true,
                basePrice: 902969002,
                active: true,
                manageItemBy: default,
                expiredType: default,
                expiredValue: 743586526,
                issueMethod: default,
                canUpdate: true,
                purUnitRate: 424110346,
                salesUnitRate: 925853544,
                itemTypeId: Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
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