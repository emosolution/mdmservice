using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPriceListsAppService _priceListsAppService;
        private readonly IRepository<PriceList, Guid> _priceListRepository;

        public PriceListsAppServiceTests()
        {
            _priceListsAppService = GetRequiredService<IPriceListsAppService>();
            _priceListRepository = GetRequiredService<IRepository<PriceList, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _priceListsAppService.GetListAsync(new GetPriceListsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PriceList.Id == Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5")).ShouldBe(true);
            result.Items.Any(x => x.PriceList.Id == Guid.Parse("93093ff1-5104-4d11-b8e1-a3d31bc38aa7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListsAppService.GetAsync(Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListCreateDto
            {
                Code = "00e50d11bcdd4cbbbd40",
                Name = "f089f4ccd2494ddfa78143570a117f83b1c804ea11864c2290bb62b4ed738eb98989b2c30dff4db",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 1197839333,
                ArithmeticFactorType = default,
                IsFirstPriceList = true
            };

            // Act
            var serviceResult = await _priceListsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("00e50d11bcdd4cbbbd40");
            result.Name.ShouldBe("f089f4ccd2494ddfa78143570a117f83b1c804ea11864c2290bb62b4ed738eb98989b2c30dff4db");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(1197839333);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsFirstPriceList.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListUpdateDto()
            {
                Code = "5326a599af444ad79070",
                Name = "94dda32689d7443099293f7699b89d3013665f475f33494d8bcaab4",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 487890853,
                ArithmeticFactorType = default,
                IsFirstPriceList = true
            };

            // Act
            var serviceResult = await _priceListsAppService.UpdateAsync(Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"), input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("5326a599af444ad79070");
            result.Name.ShouldBe("94dda32689d7443099293f7699b89d3013665f475f33494d8bcaab4");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(487890853);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsFirstPriceList.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListsAppService.DeleteAsync(Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"));

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"));

            result.ShouldBeNull();
        }
    }
}