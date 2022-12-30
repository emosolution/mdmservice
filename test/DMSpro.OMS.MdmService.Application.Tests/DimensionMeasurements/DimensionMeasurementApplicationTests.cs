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
            result.Items.Any(x => x.Id == Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("0e69697f-172d-4b28-9d6e-4bc4396613fd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dimensionMeasurementsAppService.GetAsync(Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DimensionMeasurementCreateDto
            {
                Code = "ff74af4b5a3644d6a32c",
                Name = "5ebb56351c6a4f7a98d0f948aa0ee5638e5a004a899f4f0ab3",
                Value = 328549387
            };

            // Act
            var serviceResult = await _dimensionMeasurementsAppService.CreateAsync(input);

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ff74af4b5a3644d6a32c");
            result.Name.ShouldBe("5ebb56351c6a4f7a98d0f948aa0ee5638e5a004a899f4f0ab3");
            result.Value.ToString().ShouldBe("328549387");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DimensionMeasurementUpdateDto()
            {
                Code = "cabb52f8a4484589a134",
                Name = "dced7917c31a415eaff27b0925a549e92656e9c9970940f1a3",
                Value = 355978429
            };

            // Act
            var serviceResult = await _dimensionMeasurementsAppService.UpdateAsync(Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"), input);

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("cabb52f8a4484589a134");
            result.Name.ShouldBe("dced7917c31a415eaff27b0925a549e92656e9c9970940f1a3");
            result.Value.ToString().ShouldBe("355978429");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dimensionMeasurementsAppService.DeleteAsync(Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"));

            // Assert
            var result = await _dimensionMeasurementRepository.FindAsync(c => c.Id == Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"));

            result.ShouldBeNull();
        }
    }
}