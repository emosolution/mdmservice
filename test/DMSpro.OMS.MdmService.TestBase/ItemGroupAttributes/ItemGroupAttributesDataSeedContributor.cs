using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemGroupAttributes;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

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

        public ItemGroupAttributesDataSeedContributor(IItemGroupAttributeRepository itemGroupAttributeRepository, IUnitOfWorkManager unitOfWorkManager, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor, 
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
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor; 
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

            await _itemGroupsDataSeedContributor.SeedAsync(context);
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

            await _itemGroupAttributeRepository.InsertAsync(new ItemGroupAttribute
            (
                id: Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"),
                dummy: "2702e04d2baa44f3aa88",
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
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
                attr19Id: null,
                attr5Id: null
            ));

            await _itemGroupAttributeRepository.InsertAsync(new ItemGroupAttribute
            (
                id: Guid.Parse("6df651b1-c556-409c-9f82-bc3c06109e4f"),
                dummy: "6c0058c980fb4f10b428",
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
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
                attr19Id: null,
                attr5Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}