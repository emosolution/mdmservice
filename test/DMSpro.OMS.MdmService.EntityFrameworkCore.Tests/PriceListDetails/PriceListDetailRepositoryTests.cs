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
                    description: "d76f06c53cce4b66b273813284f9a4c120f40e38bd034b2ca0260176"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"));
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
                    description: "f23907d612f14b05ad4007c1f96b7f1af7d9fe910fdd4547b60904846d7d351176c2d6faf3a64a6a8c2d282c5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}