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
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a")).ShouldBe(true);
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("814e2f11-06f4-45cf-9e36-80df209fbc15")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListDetailsAppService.GetAsync(Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListDetailCreateDto
            {
                Price = 1849898042,
                BasedOnPrice = 356112308,
                Description = "5646530f32a64453a9b4dc0da54eb37c8078b22f021e4b458dba53f65501fc3e0a70a1ec16e1455595d54c8b082c4fbe43b",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(1849898042);
            result.BasedOnPrice.ShouldBe(356112308);
            result.Description.ShouldBe("5646530f32a64453a9b4dc0da54eb37c8078b22f021e4b458dba53f65501fc3e0a70a1ec16e1455595d54c8b082c4fbe43b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListDetailUpdateDto()
            {
                Price = 2117903576,
                BasedOnPrice = 1433966232,
                Description = "6de25050d6bb41f8bc197613193c924078e7eee0f6f34cf386d4b69324e4",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.UpdateAsync(Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"), input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(2117903576);
            result.BasedOnPrice.ShouldBe(1433966232);
            result.Description.ShouldBe("6de25050d6bb41f8bc197613193c924078e7eee0f6f34cf386d4b69324e4");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListDetailsAppService.DeleteAsync(Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"));

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == Guid.Parse("0e4184d8-69c6-4e16-90a4-c44f3d08337a"));

            result.ShouldBeNull();
        }
    }
}