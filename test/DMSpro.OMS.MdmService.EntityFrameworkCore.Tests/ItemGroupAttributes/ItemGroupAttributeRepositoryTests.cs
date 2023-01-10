using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public class ItemGroupAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;

        public ItemGroupAttributeRepositoryTests()
        {
            _itemGroupAttributeRepository = GetRequiredService<IItemGroupAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupAttributeRepository.GetListAsync(
                    dummy: "2702e04d2baa44f3aa88"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6b6c5675-12e0-4835-8f68-a5c3e46e9f56"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupAttributeRepository.GetCountAsync(
                    dummy: "6c0058c980fb4f10b428"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}