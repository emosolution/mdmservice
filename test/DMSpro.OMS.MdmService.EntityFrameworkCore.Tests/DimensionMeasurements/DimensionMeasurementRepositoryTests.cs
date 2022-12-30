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
                    code: "6d781fdd16474697a99a",
                    name: "80d8ca9c9c804119aca74a70556b79155e93085e4434413b92"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"));
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
                    code: "92c6345a6f2e4d53808c",
                    name: "b4c6523644be4b0983b0e3ceaad30d9f537a96f38103422589"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}