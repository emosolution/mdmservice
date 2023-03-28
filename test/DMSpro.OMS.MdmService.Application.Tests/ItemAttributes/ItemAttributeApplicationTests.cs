using System;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemAttributesAppService _itemAttributesAppService;
        private readonly IRepository<ItemAttribute, Guid> _itemAttributeRepository;

        public ItemAttributesAppServiceTests()
        {
            _itemAttributesAppService = GetRequiredService<IItemAttributesAppService>();
            _itemAttributeRepository = GetRequiredService<IRepository<ItemAttribute, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttributesAppService.GetAsync(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttributeCreateDto
            {
                AttrNo = 12,
                AttrName = "ebb79b361f744d8191e0",
                HierarchyLevel = 1043375134,
                Active = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(12);
            result.AttrName.ShouldBe("ebb79b361f744d8191e0");
            result.HierarchyLevel.ShouldBe(1043375134);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttributeUpdateDto()
            {
                AttrNo = 10,
                AttrName = "fc5ab7b778c645599cd6",
                HierarchyLevel = 586065489,
                Active = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.UpdateAsync(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"), input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(10);
            result.AttrName.ShouldBe("fc5ab7b778c645599cd6");
            result.HierarchyLevel.ShouldBe(586065489);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttributesAppService.DeleteAsync(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));

            result.ShouldBeNull();
        }
    }
}