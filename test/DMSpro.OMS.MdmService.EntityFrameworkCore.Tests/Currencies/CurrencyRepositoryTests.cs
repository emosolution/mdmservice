using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrencyRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyRepositoryTests()
        {
            _currencyRepository = GetRequiredService<ICurrencyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _currencyRepository.GetListAsync(
                    code: "f8e59fa359dd41d8984b",
                    name: "ed63c435c5cc44b8880537f259d294a0152a95cac8a64d859c80e6d5bfd9208e0910a330cc954fc88d5bee52fc2cf83db142"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _currencyRepository.GetCountAsync(
                    code: "42772b749dce4cceaa33",
                    name: "c71b5ce289354dfe907bcaf95e89d909add159bdbb6641d388dd1b98de9d459ad39693b6a89a4fbeb668b64fad9e4bbb4cec"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}