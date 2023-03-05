using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZonesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemGroupInZonesAppService _itemGroupInZonesAppService;
        private readonly IRepository<ItemGroupInZone, Guid> _itemGroupInZoneRepository;

        public ItemGroupInZonesAppServiceTests()
        {
            _itemGroupInZonesAppService = GetRequiredService<IItemGroupInZonesAppService>();
            _itemGroupInZoneRepository = GetRequiredService<IRepository<ItemGroupInZone, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemGroupInZonesAppService.GetListAsync(new GetItemGroupInZonesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemGroupInZone.Id == Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd")).ShouldBe(true);
            result.Items.Any(x => x.ItemGroupInZone.Id == Guid.Parse("a1f03077-131f-4081-ad01-d27fae85c764")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupInZonesAppService.GetAsync(Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupInZoneCreateDto
            {
                EffectiveDate = new DateTime(2011, 7, 24),
                EndDate = new DateTime(2016, 2, 2),
                Active = true,
                Description = "eb976e4fcc4246c4ab0fe0519cd2796fbda7dbf72d62422ea3c18a8f5698329e6a452e9d5ef74d23a06b88ef36205b3ce71fd298d82c4b2facfaf61260514e7c72a1ef50c9fe4a63a0a5549f9303187a9ef1ffdb307541079a9956cd268a6c75ba76aaeb8e41457b82f3888111951bbfbdb23e687c91426eb444850f280359b6d701d570d4c9423ab8e1620d47642dcf26967661bc2a41799923a5735865a9b5e29465a5c07f46f4a49549548501f12b222b75c318c345268ab672e479deca32ac0e74028ca049e6b8f4b560385d6fd2b507b891b2994f4baa7762d28d8784098fcc71eddf8f46ee8fc3c3f0790eef2bfdff0f4622ce4a978578",
                SellingZoneId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _itemGroupInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2011, 7, 24));
            result.EndDate.ShouldBe(new DateTime(2016, 2, 2));
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("eb976e4fcc4246c4ab0fe0519cd2796fbda7dbf72d62422ea3c18a8f5698329e6a452e9d5ef74d23a06b88ef36205b3ce71fd298d82c4b2facfaf61260514e7c72a1ef50c9fe4a63a0a5549f9303187a9ef1ffdb307541079a9956cd268a6c75ba76aaeb8e41457b82f3888111951bbfbdb23e687c91426eb444850f280359b6d701d570d4c9423ab8e1620d47642dcf26967661bc2a41799923a5735865a9b5e29465a5c07f46f4a49549548501f12b222b75c318c345268ab672e479deca32ac0e74028ca049e6b8f4b560385d6fd2b507b891b2994f4baa7762d28d8784098fcc71eddf8f46ee8fc3c3f0790eef2bfdff0f4622ce4a978578");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2009, 8, 23),
                EndDate = new DateTime(2016, 4, 6),
                Active = true,
                Description = "c4f97bddb3e24568a1e32e1659e3536ee95037de0b174fb4bbce5812096ba01d40226712e18445c8ac36c1add6aa894584fa0f50374140d681c9b0fa56717aca82dfbad3dacb49ffa78fbaa01845ae71b779a7bc27d6474ab17f3e546e2d8cb161080fbc508a4630977d85c16cdd4cde7e1ee8b382af493f8530fc7d7a91b070e13fa0d15f6046d89da24593112d0994f0cfa747309a42b3a0ab7a88c232067ba0bc3a09b36c4f71a6601c6f41008c0d81adeec18c3949e592f45c58dbed2d5bb06cb469c7b9433aa0631cb70c65dad62f45358b5a664b138a808c27ab61e354736d2e8f91a744e6b20033824fae6e6908f8285f6d3f4053bd7f",
                SellingZoneId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _itemGroupInZonesAppService.UpdateAsync(Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"), input);

            // Assert
            var result = await _itemGroupInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2009, 8, 23));
            result.EndDate.ShouldBe(new DateTime(2016, 4, 6));
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("c4f97bddb3e24568a1e32e1659e3536ee95037de0b174fb4bbce5812096ba01d40226712e18445c8ac36c1add6aa894584fa0f50374140d681c9b0fa56717aca82dfbad3dacb49ffa78fbaa01845ae71b779a7bc27d6474ab17f3e546e2d8cb161080fbc508a4630977d85c16cdd4cde7e1ee8b382af493f8530fc7d7a91b070e13fa0d15f6046d89da24593112d0994f0cfa747309a42b3a0ab7a88c232067ba0bc3a09b36c4f71a6601c6f41008c0d81adeec18c3949e592f45c58dbed2d5bb06cb469c7b9433aa0631cb70c65dad62f45358b5a664b138a808c27ab61e354736d2e8f91a744e6b20033824fae6e6908f8285f6d3f4053bd7f");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupInZonesAppService.DeleteAsync(Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"));

            // Assert
            var result = await _itemGroupInZoneRepository.FindAsync(c => c.Id == Guid.Parse("72f1d715-4418-4dfe-bdff-a7ce150141bd"));

            result.ShouldBeNull();
        }
    }
}