using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IDimensionMeasurementsAppService _dimensionMeasurementsAppService;
        private readonly IRepository<DimensionMeasurement, Guid> _dimensionMeasurementRepository;

        public DimensionMeasurementsAppServiceTests()
        {
            _dimensionMeasurementsAppService = GetRequiredService<IDimensionMeasurementsAppService>();
            _dimensionMeasurementRepository = GetRequiredService<IRepository<DimensionMeasurement, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _dimensionMeasurementsAppService.GetListAsync(new GetDimensionMeasurementsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("5223866f-e112-47bd-87a1-6c65b13f8934")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dimensionMeasurementsAppService.GetAsync(Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DimensionMeasurementCreateDto
            {
                Code = "db005b23f8de46ee96e5",
                Name = "2cea9fb67f1640a086eb3dbb80b0b94069dfcf80158b474196",
                Value = 294517945
            };

            // Act
            var serviceResult = await _dimensionMeasurementsAppService.CreateAsync(input);

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("db005b23f8de46ee96e5");
            result.Name.ShouldBe("2cea9fb67f1640a086eb3dbb80b0b94069dfcf80158b474196");
            result.Value.ShouldBe(294517945);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DimensionMeasurementUpdateDto()
            {
                Code = "0d7230d0bd5a4c4497e5",
                Name = "09624d4af22c41cda35d6f0d08e4f6ee2883ae2ed4f54f22be",
                Value = 502804888
            };

            // Act
            var serviceResult = await _dimensionMeasurementsAppService.UpdateAsync(Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"), input);

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("0d7230d0bd5a4c4497e5");
            result.Name.ShouldBe("09624d4af22c41cda35d6f0d08e4f6ee2883ae2ed4f54f22be");
            result.Value.ShouldBe(502804888);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dimensionMeasurementsAppService.DeleteAsync(Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"));

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"));

            result.ShouldBeNull();
        }
    }
}