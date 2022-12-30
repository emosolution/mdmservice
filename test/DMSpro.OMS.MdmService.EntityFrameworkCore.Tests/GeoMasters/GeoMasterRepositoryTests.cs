using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMasterRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IGeoMasterRepository _geoMasterRepository;

        public GeoMasterRepositoryTests()
        {
            _geoMasterRepository = GetRequiredService<IGeoMasterRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _geoMasterRepository.GetListAsync(
                    code: "90117f80f8124f",
                    erpCode: "e96a03acdd6c499f9f63ac7fa2331acc7e1e04093d0a4c64a57c7c1e16d81270159334fc9ec24570ad86b81",
                    name: "d4c826d6696f4ef496a14671e13524f397ab85c8d06c4e9fa1d18787fcdeb03569f8107848a743649773ac190f0a6e07cae0"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _geoMasterRepository.GetCountAsync(
                    code: "b5551c727e96418081b022587a45407bb53849a3f3a4497a96f8c425be6bb83a71c41",
                    erpCode: "a2d48f6d7dd84d49bcd41bdc28545b8ebe131360e2b7494da8208f5",
                    name: "3891c874fd5f4b75b905ca07132b1275c32ea298c2894c2792a5aff73ea38a28f16e11b88afa4179bf76532edc61cbce17a2"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}