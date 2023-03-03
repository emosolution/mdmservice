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
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9")).ShouldBe(true);
            result.Items.Any(x => x.PriceListDetail.Id == Guid.Parse("7e7e2c2f-264b-49c6-8052-25bcd131f787")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListDetailsAppService.GetAsync(Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListDetailCreateDto
            {
                Price = 1815555570,
                BasedOnPrice = 2133642053,
                Description = "5ee0fc9b5fef43f694e10f81c97758c355c70fca",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                ItemId = Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(1815555570);
            result.BasedOnPrice.ShouldBe(2133642053);
            result.Description.ShouldBe("5ee0fc9b5fef43f694e10f81c97758c355c70fca");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListDetailUpdateDto()
            {
                Price = 1665595640,
                BasedOnPrice = 1063313221,
                Description = "db8ab61c2b96460496a8ba",
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                UOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                ItemId = Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4")
            };

            // Act
            var serviceResult = await _priceListDetailsAppService.UpdateAsync(Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"), input);

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Price.ShouldBe(1665595640);
            result.BasedOnPrice.ShouldBe(1063313221);
            result.Description.ShouldBe("db8ab61c2b96460496a8ba");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListDetailsAppService.DeleteAsync(Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"));

            // Assert
            var result = await _priceListDetailRepository.FindAsync(c => c.Id == Guid.Parse("6613d68d-298d-441e-9716-5eb1f3143ae9"));

            result.ShouldBeNull();
        }
    }
}