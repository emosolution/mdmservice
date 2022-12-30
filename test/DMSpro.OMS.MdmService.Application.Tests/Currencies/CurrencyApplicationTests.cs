using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Currencies
{
    public class CurrenciesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICurrenciesAppService _currenciesAppService;
        private readonly IRepository<Currency, Guid> _currencyRepository;

        public CurrenciesAppServiceTests()
        {
            _currenciesAppService = GetRequiredService<ICurrenciesAppService>();
            _currencyRepository = GetRequiredService<IRepository<Currency, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _currenciesAppService.GetListAsync(new GetCurrenciesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("f879d4a6-0695-4106-ae96-fadbc0e3fe81")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _currenciesAppService.GetAsync(Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CurrencyCreateDto
            {
                Code = "5440adb5129c4996ba07",
                Name = "8f67da051be542f2beb92113aa5b253ec214a7a75c8649629f90a276b74b70ef61267af737db48e8b29d38c5c4749f3e1d02"
            };

            // Act
            var serviceResult = await _currenciesAppService.CreateAsync(input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("5440adb5129c4996ba07");
            result.Name.ShouldBe("8f67da051be542f2beb92113aa5b253ec214a7a75c8649629f90a276b74b70ef61267af737db48e8b29d38c5c4749f3e1d02");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CurrencyUpdateDto()
            {
                Code = "c812d1c33f16417bb026",
                Name = "50ed94691acc42aea2c91308f1d5e465571af43227fe4bd4b13720af50635190c349845a6e4244a08851696f9ae21f1c83c8"
            };

            // Act
            var serviceResult = await _currenciesAppService.UpdateAsync(Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"), input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c812d1c33f16417bb026");
            result.Name.ShouldBe("50ed94691acc42aea2c91308f1d5e465571af43227fe4bd4b13720af50635190c349845a6e4244a08851696f9ae21f1c83c8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _currenciesAppService.DeleteAsync(Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"));

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == Guid.Parse("b9db60e3-0dc5-4b87-be00-55e2fbf01676"));

            result.ShouldBeNull();
        }
    }
}