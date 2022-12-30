using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListRepositoryTests()
        {
            _priceListRepository = GetRequiredService<IPriceListRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceListRepository.GetListAsync(
                    code: "1f00af0790344a42850c",
                    name: "60253ec",
                    active: true,
                    arithmeticOperation: default,
                    arithmeticFactorType: default,
                    isFirstPriceList: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceListRepository.GetCountAsync(
                    code: "da37f73422dc4d69b358",
                    name: "2874a318b89b4b7794bbdca412a5f660fb62df319c954a0f8cb2",
                    active: true,
                    arithmeticOperation: default,
                    arithmeticFactorType: default,
                    isFirstPriceList: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}