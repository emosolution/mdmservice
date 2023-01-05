using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceListDetailRepository _priceListDetailRepository;

        public PriceListDetailRepositoryTests()
        {
            _priceListDetailRepository = GetRequiredService<IPriceListDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceListDetailRepository.GetListAsync(
                    description: "d097712be59d450f937ca72bcb5554df0b277ef715d448448e261b7dd3b077935581e78e954f43c780c494dad85"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceListDetailRepository.GetCountAsync(
                    description: "16589b337a17494c88f35913b5daa0d9c89d3d369fdc4ee1b23"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}