using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemGroupLists;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        private readonly ItemsDataSeedContributor _itemsDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor;

        public ItemGroupListsDataSeedContributor(IItemGroupListRepository itemGroupListRepository, IUnitOfWorkManager unitOfWorkManager, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor, ItemsDataSeedContributor itemsDataSeedContributor, UOMsDataSeedContributor uOMsDataSeedContributor)
        {
            _itemGroupListRepository = itemGroupListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor; _itemsDataSeedContributor = itemsDataSeedContributor; _uOMsDataSeedContributor = uOMsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemGroupsDataSeedContributor.SeedAsync(context);
            await _itemsDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor.SeedAsync(context);

            await _itemGroupListRepository.InsertAsync(new ItemGroupList
            (
                id: Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"),
                rate: 1476865559,
                price: 147435680,
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"),
                uomId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _itemGroupListRepository.InsertAsync(new ItemGroupList
            (
                id: Guid.Parse("f79d3b25-7067-439c-b323-243f3b027b21"),
                rate: 1793381621,
                price: 1000491669,
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                itemId: Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"),
                uomId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}