using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.WeightMeasurements;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IWeightMeasurementRepository _weightMeasurementRepository;

        public WeightMeasurementRepositoryTests()
        {
            _weightMeasurementRepository = GetRequiredService<IWeightMeasurementRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _weightMeasurementRepository.GetListAsync(
                    code: "c9ad8ccac4564097",
                    name: "7ef9575d4b4d4d65bc22e9bf5ca83fb3a7f1562581964842be"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _weightMeasurementRepository.GetCountAsync(
                    code: "2f682a80d2e74edea",
                    name: "08464d3702d54cd9bfbe8737a129ec9fb557225dc1534b0db2"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}