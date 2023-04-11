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
                id: Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"),
                rate: 459374827,
                price: 39570775,
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                itemId: Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"),
                uomId: null
            ));

            await _itemGroupListRepository.InsertAsync(new ItemGroupList
            (
                id: Guid.Parse("3d732bba-56a0-413a-b5cb-bf75e4752fa3"),
                rate: 318406865,
                price: 2029635422,
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                itemId: Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"),
                uomId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}