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
                    description: "5bf6f0554756476ca464437e2806735a19e38123"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"));
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
                    description: "751cb588f908496992e1d99d29f1caabb9eaf3e03b93457ba5efc7aa027e418bff1951f3bf1b4c9693"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}