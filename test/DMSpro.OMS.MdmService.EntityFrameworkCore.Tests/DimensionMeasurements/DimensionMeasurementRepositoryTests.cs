using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IDimensionMeasurementRepository _dimensionMeasurementRepository;

        public DimensionMeasurementRepositoryTests()
        {
            _dimensionMeasurementRepository = GetRequiredService<IDimensionMeasurementRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dimensionMeasurementRepository.GetListAsync(
                    code: "82df60d9aab747d2ac77",
                    name: "c12281cf94f3461a9b6978068139c4aad54bc18cbce14a2683"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _dimensionMeasurementRepository.GetCountAsync(
                    code: "337cc6bfcbf842b6aeda",
                    name: "8ee2a7e7f29e4de48fad4451a1a63ef8ded22eb2ac8e43888c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}