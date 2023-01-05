using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPriceListDetailsAppService _priceListDetailsAppService;
        private readonly IRepository<PriceListDetail, Guid> _priceListDetailRepository;

        public PriceListDetailsAppServiceTests()
        {
            _priceListDetailsAppService = GetRequiredService<IPriceListDetailsAppService>();
            _priceListDetailRepository = GetRequiredService<IRepository<PriceListDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _priceListDetailsAppService.GetListAsync(new GetPriceListDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("f8268675-0260-4229-ab59-b4900bead351")).ShouldBe(true);
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("161f23e8-de0a-4c06-9e8e-7a265acf0803")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListDetailsAppService.GetAsync(Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListDetailCreateDto
            {
                Price = 1087704098,
                BasedOnPrice = 1493623584,
                Description = "856a848e5910458fa61ce9fd69919f67d21ba5e30a9f4c6c9ab4ff17ddb098735911998539314fadb805c3ff3b611a563b",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(1087704098);
            result.BasedOnPrice.ShouldBe(1493623584);
            result.Description.ShouldBe("856a848e5910458fa61ce9fd69919f67d21ba5e30a9f4c6c9ab4ff17ddb098735911998539314fadb805c3ff3b611a563b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListDetailUpdateDto()
            {
                Price = 594370578,
                BasedOnPrice = 1892408547,
                Description = "40eb4c866b8",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.UpdateAsync(Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"), input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(594370578);
            result.BasedOnPrice.ShouldBe(1892408547);
            result.Description.ShouldBe("40eb4c866b8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListDetailsAppService.DeleteAsync(Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"));

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == Guid.Parse("f8268675-0260-4229-ab59-b4900bead351"));

            result.ShouldBeNull();
        }
    }
}