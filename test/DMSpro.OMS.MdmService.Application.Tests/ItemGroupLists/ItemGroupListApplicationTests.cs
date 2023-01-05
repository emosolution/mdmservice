using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemGroupListsAppService _itemGroupListsAppService;
        private readonly IRepository<ItemGroupList, Guid> _itemGroupListRepository;

        public ItemGroupListsAppServiceTests()
        {
            _itemGroupListsAppService = GetRequiredService<IItemGroupListsAppService>();
            _itemGroupListRepository = GetRequiredService<IRepository<ItemGroupList, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemGroupListsAppService.GetListAsync(new GetItemGroupListsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemGroupList.Id == Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab")).ShouldBe(true);
            result.Items.Any(x => x.ItemGroupList.Id == Guid.Parse("f79d3b25-7067-439c-b323-243f3b027b21")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupListsAppService.GetAsync(Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupListCreateDto
            {
                Rate = 1538029355,
                Price = 1526461826,
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"),
                UomId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _itemGroupListsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(1538029355);
            result.Price.ShouldBe(1526461826);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupListUpdateDto()
            {
                Rate = 2103755667,
                Price = 2035928881,
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"),
                UomId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _itemGroupListsAppService.UpdateAsync(Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"), input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(2103755667);
            result.Price.ShouldBe(2035928881);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupListsAppService.DeleteAsync(Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"));

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == Guid.Parse("fe303538-b412-4c0f-b380-e164e2179dab"));

            result.ShouldBeNull();
        }
    }
}