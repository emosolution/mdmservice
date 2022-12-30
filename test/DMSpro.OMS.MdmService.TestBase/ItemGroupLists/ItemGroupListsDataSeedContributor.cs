using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.ItemMasters;
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

        private readonly ItemMastersDataSeedContributor _itemMastersDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSeedContributor;

        public ItemGroupListsDataSeedContributor(IItemGroupListRepository itemGroupListRepository, IUnitOfWorkManager unitOfWorkManager, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor, ItemMastersDataSeedContributor itemMastersDataSeedContributor, UOMsDataSeedContributor uOMsDataSeedContributor)
        {
            _itemGroupListRepository = itemGroupListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor; _itemMastersDataSeedContributor = itemMastersDataSeedContributor; _uOMsDataSeedContributor = uOMsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemGroupsDataSeedContributor.SeedAsync(context);
            await _itemMastersDataSeedContributor.SeedAsync(context);
            await _uOMsDataSeedContributor.SeedAsync(context);

            await _itemGroupListRepository.InsertAsync(new ItemGroupList
            (
                id: Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"),
                rate: 715514576,
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                itemId: Guid.Parse("fc6c541e-513e-4827-8fca-c4cce37b3c35"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _itemGroupListRepository.InsertAsync(new ItemGroupList
            (
                id: Guid.Parse("72db43ef-f0bd-4fea-a5cf-d35748eaa34f"),
                rate: 1799215464,
                itemGroupId: Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                itemId: Guid.Parse("fc6c541e-513e-4827-8fca-c4cce37b3c35"),
                uOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}