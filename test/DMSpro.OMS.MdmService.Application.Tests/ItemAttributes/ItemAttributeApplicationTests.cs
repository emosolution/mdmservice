using System;
using System.Linq;
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
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemAttributesAppService.GetListAsync(new GetItemAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("d3b859c6-5b84-454c-a914-68680e430af5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttributesAppService.GetAsync(Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttributeCreateDto
            {
                AttrNo = 2,
                AttrName = "04e16c4ea1cf4dfaae70",
                HierarchyLevel = 1325607738,
                Active = true,
                IsSellingCategory = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(2);
            result.AttrName.ShouldBe("04e16c4ea1cf4dfaae70");
            result.HierarchyLevel.ShouldBe(1325607738);
            result.Active.ShouldBe(true);
            result.IsSellingCategory.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttributeUpdateDto()
            {
                AttrNo = 15,
                AttrName = "1105b8c5666347a6964b",
                HierarchyLevel = 1250698271,
                Active = true,
                IsSellingCategory = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.UpdateAsync(Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"), input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(15);
            result.AttrName.ShouldBe("1105b8c5666347a6964b");
            result.HierarchyLevel.ShouldBe(1250698271);
            result.Active.ShouldBe(true);
            result.IsSellingCategory.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttributesAppService.DeleteAsync(Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"));

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"));

            result.ShouldBeNull();
        }
    }
}