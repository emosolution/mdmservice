using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemGroupAttributesAppService _itemGroupAttributesAppService;
        private readonly IRepository<ItemGroupAttribute, Guid> _itemGroupAttributeRepository;

        public ItemGroupAttributesAppServiceTests()
        {
            _itemGroupAttributesAppService = GetRequiredService<IItemGroupAttributesAppService>();
            _itemGroupAttributeRepository = GetRequiredService<IRepository<ItemGroupAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemGroupAttributesAppService.GetListAsync(new GetItemGroupAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemGroupAttribute.Id == Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56")).ShouldBe(true);
            result.Items.Any(x => x.ItemGroupAttribute.Id == Guid.Parse("6df651b1-c556-409c-9f82-bc3c06109e4f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupAttributesAppService.GetAsync(Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupAttributeCreateDto
            {
                dummy = "53ddba59b8c4445ca13f",
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),

            };

            // Act
            var serviceResult = await _itemGroupAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.dummy.ShouldBe("53ddba59b8c4445ca13f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupAttributeUpdateDto()
            {
                dummy = "2c8d4d208a78458ca2ea",
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),

            };

            // Act
            var serviceResult = await _itemGroupAttributesAppService.UpdateAsync(Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"), input);

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.dummy.ShouldBe("2c8d4d208a78458ca2ea");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupAttributesAppService.DeleteAsync(Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"));

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"));

            result.ShouldBeNull();
        }
    }
}