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
                    description: "8ac9e1451f3849c0af1d3f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"));
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
                    description: "b69cba5b86aa48cf8ea8440f792e294a69c238b342fa405e9862da18d780d7466271"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}