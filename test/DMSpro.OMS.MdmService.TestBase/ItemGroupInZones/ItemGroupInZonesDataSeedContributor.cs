using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemGroupInZones;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZonesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemGroupInZoneRepository _itemGroupInZoneRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SalesOrgHierarchiesDataSeedContributor _salesOrgHierarchiesDataSeedContributor;

        private readonly ItemGroupsDataSeedContributor _itemGroupsDataSeedContributor;

        public ItemGroupInZonesDataSeedContributor(IItemGroupInZoneRepository itemGroupInZoneRepository, IUnitOfWorkManager unitOfWorkManager, SalesOrgHierarchiesDataSeedContributor salesOrgHierarchiesDataSeedContributor, ItemGroupsDataSeedContributor itemGroupsDataSeedContributor)
        {
            _itemGroupInZoneRepository = itemGroupInZoneRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salesOrgHierarchiesDataSeedContributor = salesOrgHierarchiesDataSeedContributor; _itemGroupsDataSeedContributor = itemGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHierarchiesDataSeedContributor.SeedAsync(context);
            await _itemGroupsDataSeedContributor.SeedAsync(context);

            await _itemGroupInZoneRepository.InsertAsync(new ItemGroupInZone
            (
                id: Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"),
                effectiveDate: new DateTime(2021, 3, 22),
                endDate: new DateTime(2003, 9, 3),
                active: true,
                description: "846b9a993ab244e9b82c930b523218b7adc022dbce504e2b80f683e0ab4f0b0199d60122e2ca4f40b15ec4a2c070c8a17aca23a6fe7749d69c91d0d2633cca390da91aeaee9b427596ee2268cec8fecb9c10e203e2be4eca82ea6b5180bf3eaddd205b7cfd9443f381a45a28564214c266e3f6d21ee84fd197d0205ff0fbdc35e78cc3503a284d46aca60e12c4de1dfb68b7e0ea3ffb4ea182f27c3b0e8f2b3b00f83f3b176e408b81ce925c6d5edca506a941495be44c5aaab755f0ec8b9a502e0580289e564ef5b479a9e9d7086acd99496814ab3842079fa5c4e1fcec17c6c3443e3ed86442f688fb96e77546088e99ec2fc998014ed4ae77",
                sellingZoneId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _itemGroupInZoneRepository.InsertAsync(new ItemGroupInZone
            (
                id: Guid.Parse("a1f03077-131f-4081-ad01-d27fae85c764"),
                effectiveDate: new DateTime(2009, 5, 7),
                endDate: new DateTime(2002, 5, 25),
                active: true,
                description: "2135076002e440a19ed2b2fd75e21baed1b157569e8e45618299db12b9eea654a5464238beb94a7684c1793a215a31c0e3363ec6272640dfa65aa1d7b985097c3306cbc5a85d4a65996c35630ee81eb60f0055ccc1584944959b860827353fb4929a0602097e40a6bb12ddf6a0534baaca374e2e5f8e4f60ae7a852251137022386260ead0be41239fcbf5283a9a86164451a363afe9481fbb8af80ccbc2c9b0ce6b7627e60e46e496a4789054243c5efdef099060f64a8dbf2f6538448d2d067e5e97385c2d454682d58b436c3a77745e903f09daf345cca9356b718174a95a708f1ea0e9484dfab1bac85c7a20064d9fce65e43d7e468194f8",
                sellingZoneId: Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                itemGroupId: Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}