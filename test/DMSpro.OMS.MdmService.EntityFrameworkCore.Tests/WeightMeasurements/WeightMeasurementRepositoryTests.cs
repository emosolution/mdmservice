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
                    code: "cd5c15d8ddf94ce3b45",
                    name: "9e08a98396fd460b9939e975606c5465cc352d31c63d41968f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"));
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
                    code: "7dfb3bb1bf8b4697",
                    name: "bff152df46684112bd2e3810be181037b640c3c2a9894ce8b1"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}