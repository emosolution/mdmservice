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
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46")).ShouldBe(true);
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("be683c3f-83de-422b-8e19-34743edd5107")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListDetailsAppService.GetAsync(Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListDetailCreateDto
            {
                Price = 572125898,
                BasedOnPrice = 1768243427,
                Description = "4a0b094455124da8a7920c7bd0ca6",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                ItemMasterId = Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(572125898);
            result.BasedOnPrice.ShouldBe(1768243427);
            result.Description.ShouldBe("4a0b094455124da8a7920c7bd0ca6");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListDetailUpdateDto()
            {
                Price = 1723192200,
                BasedOnPrice = 352772975,
                Description = "2aa37dc4285b40",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                ItemMasterId = Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.UpdateAsync(Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"), input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(1723192200);
            result.BasedOnPrice.ShouldBe(352772975);
            result.Description.ShouldBe("2aa37dc4285b40");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListDetailsAppService.DeleteAsync(Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"));

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == Guid.Parse("6ddbcaf0-e4ed-47f2-a75d-fe074f949c46"));

            result.ShouldBeNull();
        }
    }
}