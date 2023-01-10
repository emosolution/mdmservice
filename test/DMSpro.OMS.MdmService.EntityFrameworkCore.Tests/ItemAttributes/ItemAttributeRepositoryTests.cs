using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeRepositoryTests()
        {
            _itemAttributeRepository = GetRequiredService<IItemAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemAttributeRepository.GetListAsync(
                    attrName: "601d6242fcbb447b8d0b",
                    active: true,
                    isSellingCategory: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemAttributeRepository.GetCountAsync(
                    attrName: "e0f2827c1ba846ad8edf",
                    active: true,
                    isSellingCategory: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}