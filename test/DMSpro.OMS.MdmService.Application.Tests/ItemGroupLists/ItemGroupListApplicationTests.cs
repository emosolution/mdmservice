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
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupListsAppService.GetAsync(Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupListCreateDto
            {
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),
                ItemId = Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"),

            };

            // Act
            var serviceResult = await _itemGroupListsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(null);
            result.Price.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupListUpdateDto()
            {
                ItemId = Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"),

            };

            // Act
            var serviceResult = await _itemGroupListsAppService.UpdateAsync(Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"), input);

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Rate.ShouldBe(null);
            result.Price.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupListsAppService.DeleteAsync(Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"));

            // Assert
            var result = await _itemGroupListRepository.FindAsync(c => c.Id == Guid.Parse("cd411e28-324b-4dc3-b152-4845bd1fbd62"));

            result.ShouldBeNull();
        }
    }
}