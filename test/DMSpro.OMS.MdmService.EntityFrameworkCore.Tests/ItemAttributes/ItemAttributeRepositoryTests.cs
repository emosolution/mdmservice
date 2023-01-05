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
                    attrNo: "2cd9bbff3ab948e5a83e",
                    attrName: "8ef91d393d6947069f1e",
                    active: true,
                    isSellingCategory: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"));
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
                    attrNo: "c61b88a8e7ec45ab8944",
                    attrName: "7328332efef9415f93b7",
                    active: true,
                    isSellingCategory: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}