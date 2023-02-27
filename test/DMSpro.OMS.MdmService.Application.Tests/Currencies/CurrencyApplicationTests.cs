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
            result.Items.Any(x => x.Id == Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("9b21e357-66b2-4b0c-a7a5-e01349305c45")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _currenciesAppService.GetAsync(Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CurrencyCreateDto
            {
                Code = "4e357558650b4e4b9893",
                Name = "d3c198ca2d564113b77ff06c8546f8709981dd730ba8486c91ad9f9cafd263e0f04b380893c5483c8f60e4782d145e5ba56b"
            };

            // Act
            var serviceResult = await _currenciesAppService.CreateAsync(input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("4e357558650b4e4b9893");
            result.Name.ShouldBe("d3c198ca2d564113b77ff06c8546f8709981dd730ba8486c91ad9f9cafd263e0f04b380893c5483c8f60e4782d145e5ba56b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CurrencyUpdateDto()
            {
                Code = "ff15e707b70c4faab087",
                Name = "45b7ab11fd5142d7abc028ca4e9ce6c3d01a7eb8aa624fdea75d870554222c31f4dd6de3181e4815954aaf643987db35a35a"
            };

            // Act
            var serviceResult = await _currenciesAppService.UpdateAsync(Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"), input);

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ff15e707b70c4faab087");
            result.Name.ShouldBe("45b7ab11fd5142d7abc028ca4e9ce6c3d01a7eb8aa624fdea75d870554222c31f4dd6de3181e4815954aaf643987db35a35a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _currenciesAppService.DeleteAsync(Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"));

            // Assert
            var result = await _currencyRepository.FindAsync(c => c.Id == Guid.Parse("90d6126f-c3e7-45d4-9cae-4898bcb313cd"));

            result.ShouldBeNull();
        }
    }
}