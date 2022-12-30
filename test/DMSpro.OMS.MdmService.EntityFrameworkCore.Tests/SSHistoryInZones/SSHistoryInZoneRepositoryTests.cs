using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SSHistoryInZones;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class SSHistoryInZoneRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISSHistoryInZoneRepository _sSHistoryInZoneRepository;

        public SSHistoryInZoneRepositoryTests()
        {
            _sSHistoryInZoneRepository = GetRequiredService<ISSHistoryInZoneRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _sSHistoryInZoneRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _sSHistoryInZoneRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}