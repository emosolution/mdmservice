using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigRepositoryTests()
        {
            _numberingConfigRepository = GetRequiredService<INumberingConfigRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetListAsync(
                    prefix: "1b5c519893704503a0cc48f9967a20dfa681d0b9c",
                    suffix: "8e219193e12d4828822202f61229e8e57ae95c59e93342ef98c90893e3d3975f2509ee8e1e854e92"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetCountAsync(
                    prefix: "18fdec2d7b23403e8913f8f9af7c164597fc0ed8e43c4383b2ba028f4",
                    suffix: "81edc3cfd4ff4e8da68126914391c39f30041051520343258791d"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}