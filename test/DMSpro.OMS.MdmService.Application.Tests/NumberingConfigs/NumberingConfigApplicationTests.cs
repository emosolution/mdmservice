using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly INumberingConfigsAppService _numberingConfigsAppService;
        private readonly IRepository<NumberingConfig, Guid> _numberingConfigRepository;

        public NumberingConfigsAppServiceTests()
        {
            _numberingConfigsAppService = GetRequiredService<INumberingConfigsAppService>();
            _numberingConfigRepository = GetRequiredService<IRepository<NumberingConfig, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                Prefix = "6be092801c12473c97c1",
                Suffix = "56a60e1f12e041fbb063",
                PaddingZeroNumber = 1979550699,
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Prefix.ShouldBe("6be092801c12473c97c1");
            result.Suffix.ShouldBe("56a60e1f12e041fbb063");
            result.PaddingZeroNumber.ShouldBe(1979550699);
            result.Description.ShouldBe("8c824c7775fb400b922a0252165c1808f83748a7d38145e698d510525eeca2cadf68e2db9be9428a8a6215851edddfb76783c198981f4b8ebad28051eea78afd74761b43633c4e4cb1aa13a8d16c8751a6f181c49d2c4869acd6094591e6768182076bee23984e269c5b22e64df7049c8c818d3d6dfe48b5814cd3ded6bbcad");
        }
    }
}