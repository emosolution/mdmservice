using System;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPriceUpdateDetailsAppService _priceUpdateDetailsAppService;
        private readonly IRepository<PriceUpdateDetail, Guid> _priceUpdateDetailRepository;

        public PriceUpdateDetailsAppServiceTests()
        {
            _priceUpdateDetailsAppService = GetRequiredService<IPriceUpdateDetailsAppService>();
            _priceUpdateDetailRepository = GetRequiredService<IRepository<PriceUpdateDetail, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceUpdateDetailsAppService.GetAsync(Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceUpdateDetailCreateDto
            {
                NewPrice = 1402327362,
                PriceUpdateId = Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                PriceListDetailId = Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")
            };

            // Act
            var serviceResult = await _priceUpdateDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceUpdateDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NewPrice.ShouldBe(1402327362);
            result.UpdatedDate.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceUpdateDetailUpdateDto()
            {
                NewPrice = 1184535564,
            };

            // Act
            var serviceResult = await _priceUpdateDetailsAppService.UpdateAsync(Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"), input);

            // Assert
            var result = await _priceUpdateDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NewPrice.ShouldBe(1184535564);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceUpdateDetailsAppService.DeleteAsync(Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"));

            // Assert
            var result = await _priceUpdateDetailRepository.FindAsync(c => c.Id == Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"));

            result.ShouldBeNull();
        }
    }
}