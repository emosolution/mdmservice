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
                    prefix: "1761aae285984c2781fe",
                    suffix: "60aae8ed4f794f4b8a06",
                    active: true,
                    description: "65584192913c48ba8594082831da1285d6c5abc0dcec4b899081588446797136f96233766cbc48d199be06d9305929790794a8c4324e4a1d932e6d6cd688210f67309852c1804e9a919c7c9b44f225bf7c858103fa034451a6a16afb728e441a23334778c92d4a43a2c959ad1f4731e284f43dbf1d564f4e84c2a7b69509930",
                    isDefault: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"));
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
                    prefix: "3e9ba95c4fd34026bb22",
                    suffix: "09d34815caac4db098ac",
                    active: true,
                    description: "b4f4b15c015949cd98146f92f30ba7c7f1d43cb260d744869d86bcdda1ecb73770d506ecf3ce4f38b110ef677b64af8644fdefa8627a47b994b7f24360a91f90ceddbe7157fd4c55b41a8dd75b23ebc841685763c5d545d9aece7665811bcc56f2e0d311408a412c8afa470168ca91557469e4d19e6542fdbfb6be37927a660",
                    isDefault: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}