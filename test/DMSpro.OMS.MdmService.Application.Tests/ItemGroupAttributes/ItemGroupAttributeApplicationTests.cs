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
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupAttributesAppService.GetAsync(Guid.Parse("2f32f56e-e4b3-4ae8-acbc-b6c91dab8c37"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2f32f56e-e4b3-4ae8-acbc-b6c91dab8c37"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupAttributeCreateDto
            {
                Description = "2395209f76e44e4a85f5",
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"),

            };

            // Act
            var serviceResult = await _itemGroupAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("2395209f76e44e4a85f5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupAttributeUpdateDto()
            {
                Description = "b9cf25059a8b4a4d829d",
            };

            // Act
            var serviceResult = await _itemGroupAttributesAppService.UpdateAsync(Guid.Parse("2f32f56e-e4b3-4ae8-acbc-b6c91dab8c37"), input);

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("b9cf25059a8b4a4d829d");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupAttributesAppService.DeleteAsync(Guid.Parse("2f32f56e-e4b3-4ae8-acbc-b6c91dab8c37"));

            // Assert
            var result = await _itemGroupAttributeRepository.FindAsync(c => c.Id == Guid.Parse("2f32f56e-e4b3-4ae8-acbc-b6c91dab8c37"));

            result.ShouldBeNull();
        }
    }
}