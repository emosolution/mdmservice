using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroupInZones;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupInZoneRepository _itemGroupInZoneRepository;

        public ItemGroupInZoneRepositoryTests()
        {
            _itemGroupInZoneRepository = GetRequiredService<IItemGroupInZoneRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupInZoneRepository.GetListAsync(
                    active: true,
                    description: "846b9a993ab244e9b82c930b523218b7adc022dbce504e2b80f683e0ab4f0b0199d60122e2ca4f40b15ec4a2c070c8a17aca23a6fe7749d69c91d0d2633cca390da91aeaee9b427596ee2268cec8fecb9c10e203e2be4eca82ea6b5180bf3eaddd205b7cfd9443f381a45a28564214c266e3f6d21ee84fd197d0205ff0fbdc35e78cc3503a284d46aca60e12c4de1dfb68b7e0ea3ffb4ea182f27c3b0e8f2b3b00f83f3b176e408b81ce925c6d5edca506a941495be44c5aaab755f0ec8b9a502e0580289e564ef5b479a9e9d7086acd99496814ab3842079fa5c4e1fcec17c6c3443e3ed86442f688fb96e77546088e99ec2fc998014ed4ae77"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupInZoneRepository.GetCountAsync(
                    active: true,
                    description: "2135076002e440a19ed2b2fd75e21baed1b157569e8e45618299db12b9eea654a5464238beb94a7684c1793a215a31c0e3363ec6272640dfa65aa1d7b985097c3306cbc5a85d4a65996c35630ee81eb60f0055ccc1584944959b860827353fb4929a0602097e40a6bb12ddf6a0534baaca374e2e5f8e4f60ae7a852251137022386260ead0be41239fcbf5283a9a86164451a363afe9481fbb8af80ccbc2c9b0ce6b7627e60e46e496a4789054243c5efdef099060f64a8dbf2f6538448d2d067e5e97385c2d454682d58b436c3a77745e903f09daf345cca9356b718174a95a708f1ea0e9484dfab1bac85c7a20064d9fce65e43d7e468194f8"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}