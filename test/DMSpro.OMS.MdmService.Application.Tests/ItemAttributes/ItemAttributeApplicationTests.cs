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
            result.Items.Any(x => x.Id == Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("308c03f7-7ce6-4f77-821c-601577c38001")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttributesAppService.GetAsync(Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttributeCreateDto
            {
                AttrNo = "69a0120576f1404ba36b",
                AttrName = "6892ed684e7e40a1bbba",
                HierarchyLevel = 1165194849,
                Active = true,
                IsSellingCategory = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe("69a0120576f1404ba36b");
            result.AttrName.ShouldBe("6892ed684e7e40a1bbba");
            result.HierarchyLevel.ShouldBe(1165194849);
            result.Active.ShouldBe(true);
            result.IsSellingCategory.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttributeUpdateDto()
            {
                AttrNo = "563a228672264ffb8538",
                AttrName = "b1382b1024d04c4799b1",
                HierarchyLevel = 1189393895,
                Active = true,
                IsSellingCategory = true
            };

            // Act
            var serviceResult = await _itemAttributesAppService.UpdateAsync(Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"), input);

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe("563a228672264ffb8538");
            result.AttrName.ShouldBe("b1382b1024d04c4799b1");
            result.HierarchyLevel.ShouldBe(1189393895);
            result.Active.ShouldBe(true);
            result.IsSellingCategory.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttributesAppService.DeleteAsync(Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"));

            // Assert
            var result = await _itemAttributeRepository.FindAsync(c => c.Id == Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"));

            result.ShouldBeNull();
        }
    }
}