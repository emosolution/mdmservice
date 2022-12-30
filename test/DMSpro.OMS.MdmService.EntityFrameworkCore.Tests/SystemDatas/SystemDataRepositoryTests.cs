using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDataRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISystemDataRepository _systemDataRepository;

        public SystemDataRepositoryTests()
        {
            _systemDataRepository = GetRequiredService<ISystemDataRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _systemDataRepository.GetListAsync(
                    code: "cfabd310ae",
                    valueCode: "c7d5a642e3",
                    valueName: "593efc875800423e854ff83923878c221b3149124fed49599e41130748a900562a7ab61e468b45a68755888f05ab5a8ed97c"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _systemDataRepository.GetCountAsync(
                    code: "64fb608af5",
                    valueCode: "d1ccb117e5",
                    valueName: "c8d8e4421ebe4daebacc2091b30fafecb46079221c1e4ef4b6b8bbe8980ff6e452f0b87455c345bba867999177d425f82704"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}