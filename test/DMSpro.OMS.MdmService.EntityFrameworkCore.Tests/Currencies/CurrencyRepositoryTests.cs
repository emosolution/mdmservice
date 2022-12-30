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
                    code: "0c7175bbe58d42db9133",
                    name: "bc05aa92eadd42a19c098ffd52269cd95716eba9eff7452fa3de16a7719e529d788f4fc4c7034f08934d3495b875f87a8181"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"));
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
                    code: "59d29f6da1024153bf34",
                    name: "0ba4c5bb6865482889d580c6fe4c6adc03be00d6b13640689d9c2e42822ab871055670ef8959490b9e5f0821047bd0634d9f"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}