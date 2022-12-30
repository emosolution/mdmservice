using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemImageRepository _itemImageRepository;

        public ItemImageRepositoryTests()
        {
            _itemImageRepository = GetRequiredService<IItemImageRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemImageRepository.GetListAsync(
                    description: "949e8480d7b441d0a0409838a7fb9546e7aaddb95bca4bdcaeb88d4b8b0adb6ee0ddf89741664f368948",
                    active: true,
                    uRL: "2ae783dcdf"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemImageRepository.GetCountAsync(
                    description: "dd3067b3645744b48e0e3eb49bf858f2f78695024af94d98ab6a7650dd",
                    active: true,
                    uRL: "7610791fd19c48519ad7b77630b7a8bec0cd66f50a124f09affbfa922df814d5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}