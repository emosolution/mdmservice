using System;
using System.Linq;
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
        public async Task GetListAsync()
        {
            // Act
            var result = await _priceUpdateDetailsAppService.GetListAsync(new GetPriceUpdateDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PriceUpdateDetail.Id == Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87")).ShouldBe(true);
            result.Items.Any(x => x.PriceUpdateDetail.Id == Guid.Parse("d0e186ba-69f1-4963-805f-e5eea4faef91")).ShouldBe(true);
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
                PriceBeforeUpdate = 1301544930,
                NewPrice = 1402327362,
                UpdatedDate = new DateTime(2008, 1, 3),
                PriceUpdateId = Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                PriceListDetailId = Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")
            };

            // Act
            var serviceResult = await _priceUpdateDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceUpdateDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.PriceBeforeUpdate.ShouldBe(1301544930);
            result.NewPrice.ShouldBe(1402327362);
            result.UpdatedDate.ShouldBe(new DateTime(2008, 1, 3));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceUpdateDetailUpdateDto()
            {
                PriceBeforeUpdate = 1734307372,
                NewPrice = 1184535564,
                UpdatedDate = new DateTime(2005, 2, 4),
                PriceUpdateId = Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"),
                PriceListDetailId = Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")
            };

            // Act
            var serviceResult = await _priceUpdateDetailsAppService.UpdateAsync(Guid.Parse("16c1cd30-16ac-4c30-89b6-87018568cb87"), input);

            // Assert
            var result = await _priceUpdateDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.PriceBeforeUpdate.ShouldBe(1734307372);
            result.NewPrice.ShouldBe(1184535564);
            result.UpdatedDate.ShouldBe(new DateTime(2005, 2, 4));
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