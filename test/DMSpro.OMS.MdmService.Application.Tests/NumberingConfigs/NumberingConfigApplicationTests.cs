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
        public async Task GetListAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetListAsync(new GetNumberingConfigsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("0766bc5f-e627-4487-b6fe-f0c7b70568f4")).ShouldBe(true);
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
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                Prefix = "95326f43c55e44eaaef9",
                Suffix = "41338d84f80f49bab0ac",
                PaddingZeroNumber = 1771669533,
                Description = "56e06bae132e41a79dcd9b45c642753b4311ed92e1894fcfa17ec2986627107d75f90a16754642c3a138eff5e64b08ae072ae29c3ab744999bae06492ac3ef3b817086f9b3214b81822e0f74e47fcfa4d443a67f467e41648453717542cde78f2cbc840b09db41cba5dcd071b2b1d48666bd8218dd5e44f0a9d603da4e1e570"
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Prefix.ShouldBe("95326f43c55e44eaaef9");
            result.Suffix.ShouldBe("41338d84f80f49bab0ac");
            result.PaddingZeroNumber.ShouldBe(1771669533);
            result.Description.ShouldBe("56e06bae132e41a79dcd9b45c642753b4311ed92e1894fcfa17ec2986627107d75f90a16754642c3a138eff5e64b08ae072ae29c3ab744999bae06492ac3ef3b817086f9b3214b81822e0f74e47fcfa4d443a67f467e41648453717542cde78f2cbc840b09db41cba5dcd071b2b1d48666bd8218dd5e44f0a9d603da4e1e570");
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
                Description = "8c824c7775fb400b922a0252165c1808f83748a7d38145e698d510525eeca2cadf68e2db9be9428a8a6215851edddfb76783c198981f4b8ebad28051eea78afd74761b43633c4e4cb1aa13a8d16c8751a6f181c49d2c4869acd6094591e6768182076bee23984e269c5b22e64df7049c8c818d3d6dfe48b5814cd3ded6bbcad"
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

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"));

            result.ShouldBeNull();
        }
    }
}