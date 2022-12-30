using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;

        public PriceUpdateRepositoryTests()
        {
            _priceUpdateRepository = GetRequiredService<IPriceUpdateRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceUpdateRepository.GetListAsync(
                    code: "b8e4af2d81e64cf9b46f",
                    description: "3c12564c303c4379ab3ad530c0ad48b733be62311d974d71a1dc71f1d7274cb",
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceUpdateRepository.GetCountAsync(
                    code: "a944fc77cca74ec9bc4f",
                    description: "e15c75ae0a074b9e9f81ff83129105e8a02",
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}