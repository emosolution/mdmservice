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

        //[Fact]
        //public async Task GetAsync()
        //{
        //    // Act
        //    var result = await _itemAttributesAppService.GetAsync(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));

        //    // Assert
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldBe(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));
        //}

        //[Fact]
        //public async Task UpdateAsync()
        //{
        //    // Arrange
        //    var input = new ItemAttributeUpdateDto()
        //    {
        //        AttrName = "fc5ab7b778c645599cd6",
        //    };

        //    // Act
        //    var serviceResult = await _itemAttributesAppService.UpdateAsync(Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"), input);

        //    // Assert
        //    var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

        //    result.ShouldNotBe(null);
        //    result.AttrName.ShouldBe("fc5ab7b778c645599cd6");
        //}

        //[Fact]
        //public async Task DeleteAsync()
        //{
        //    // Act
        //    await _itemAttributesAppService.DeleteAsync();

        //    // Assert
        //    var result = await _itemAttributeRepository.FindAsync(c => c.Id == Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"));

        //    result.ShouldBeNull();
        //}
    }
}