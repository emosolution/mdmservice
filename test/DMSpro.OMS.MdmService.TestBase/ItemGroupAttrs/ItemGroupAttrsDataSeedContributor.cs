using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemGroupAttrs;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttrsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemGroupAttrRepository _itemGroupAttrRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues0DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues1DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues2DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues3DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues4DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues5DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues6DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues7DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues8DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues9DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues10DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues11DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues12DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues13DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues14DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues15DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues16DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues17DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues18DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues19DataSeedContributor;

        public ItemGroupAttrsDataSeedContributor(IItemGroupAttrRepository itemGroupAttrRepository, IUnitOfWorkManager unitOfWorkManager, 
            ItemGroupsDataSeedContributor itemGroupsDataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues0DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues1DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues2DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues3DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues4DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues5DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues6DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues7DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues8DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues9DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues10DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues11DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues12DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues13DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues14DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues15DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues16DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues17DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues18DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues19DataSeedContributor)
        {
            _itemGroupAttrRepository = itemGroupAttrRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor; 
            _prodAttributeValues0DataSeedContributor = prodAttributeValues0DataSeedContributor; 
            _prodAttributeValues1DataSeedContributor = prodAttributeValues1DataSeedContributor; 
            _prodAttributeValues2DataSeedContributor = prodAttributeValues2DataSeedContributor; 
            _prodAttributeValues3DataSeedContributor = prodAttributeValues3DataSeedContributor; 
            _prodAttributeValues4DataSeedContributor = prodAttributeValues4DataSeedContributor; 
            _prodAttributeValues5DataSeedContributor = prodAttributeValues5DataSeedContributor; 
            _prodAttributeValues6DataSeedContributor = prodAttributeValues6DataSeedContributor; 
            _prodAttributeValues7DataSeedContributor = prodAttributeValues7DataSeedContributor; 
            _prodAttributeValues8DataSeedContributor = prodAttributeValues8DataSeedContributor; 
            _prodAttributeValues9DataSeedContributor = prodAttributeValues9DataSeedContributor; 
            _prodAttributeValues10DataSeedContributor = prodAttributeValues10DataSeedContributor; 
            _prodAttributeValues11DataSeedContributor = prodAttributeValues11DataSeedContributor; 
            _prodAttributeValues12DataSeedContributor = prodAttributeValues12DataSeedContributor; 
            _prodAttributeValues13DataSeedContributor = prodAttributeValues13DataSeedContributor; 
            _prodAttributeValues14DataSeedContributor = prodAttributeValues14DataSeedContributor; 
            _prodAttributeValues15DataSeedContributor = prodAttributeValues15DataSeedContributor; 
            _prodAttributeValues16DataSeedContributor = prodAttributeValues16DataSeedContributor; 
            _prodAttributeValues17DataSeedContributor = prodAttributeValues17DataSeedContributor; 
            _prodAttributeValues18DataSeedContributor = prodAttributeValues18DataSeedContributor; 
            _prodAttributeValues19DataSeedContributor = prodAttributeValues19DataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemGroupsDataSeedContributor.SeedAsync(context);
            await _prodAttributeValues0DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues1DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues2DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues3DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues4DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues5DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues6DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues7DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues8DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues9DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues10DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues11DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues12DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues13DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues14DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues15DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues16DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues17DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues18DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues19DataSeedContributor.SeedAsync(context);

            await _itemGroupAttrRepository.InsertAsync(new ItemGroupAttr
            (
                id: Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"),
                dummy: true,
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                attr0: null,
                attr1: null,
                attr2: null,
                attr3: null,
                attr4: null,
                attr5: null,
                attr6: null,
                attr7: null,
                attr8: null,
                attr9: null,
                attr10: null,
                attr11: null,
                attr12: null,
                attr13: null,
                attr14: null,
                attr15: null,
                attr16: null,
                attr17: null,
                attr18: null,
                attr19: null
            ));

            await _itemGroupAttrRepository.InsertAsync(new ItemGroupAttr
            (
                id: Guid.Parse("bf573342-35ca-4344-bac3-74cb3243c0f5"),
                dummy: true,
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                attr0: null,
                attr1: null,
                attr2: null,
                attr3: null,
                attr4: null,
                attr5: null,
                attr6: null,
                attr7: null,
                attr8: null,
                attr9: null,
                attr10: null,
                attr11: null,
                attr12: null,
                attr13: null,
                attr14: null,
                attr15: null,
                attr16: null,
                attr17: null,
                attr18: null,
                attr19: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}