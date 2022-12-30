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
            result.Items.Any(x => x.ItemGroupList.Id == Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e")).ShouldBe(true);
            result.Items.Any(x => x.ItemGroupList.Id == Guid.Parse("72db43ef-f0bd-4fea-a5cf-d35748eaa34f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupListsAppService.GetAsync(Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupListCreateDto
            {
                Rate = 972047331,
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                ItemId = Guid.Parse("fc6c541e-513e-4827-8fca-c4cce37b3c35"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _itemGroupListsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(972047331);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupListUpdateDto()
            {
                Rate = 1741768139,
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                ItemId = Guid.Parse("fc6c541e-513e-4827-8fca-c4cce37b3c35"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _itemGroupListsAppService.UpdateAsync(Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"), input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(1741768139);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupListsAppService.DeleteAsync(Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"));

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == Guid.Parse("e9a1b7a8-4127-4dd8-a8af-a8ae83e23e7e"));

            result.ShouldBeNull();
        }
    }
}