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
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("56a87130-e867-4adc-8f16-cfae98c4a319")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                StartNumber = 1797352907,
                Prefix = "918c3ec575ae4ba6aecb",
                Suffix = "e4800abd75114bf7ac46",
                Length = 524325223,
                Active = true,
                Description = "4d720863722c4721a747a02a4004567b59202135eafa4d149aff19eceaf6ee022279ec3142a04e5dab0d9c38f0224631a474e51bcdae4163ac343057a77f0e93c4a8bf76327042f4a10c2bcf35c892b103c3eb2d8b2f48399af1be16ec587319f86d26a5e0914a048051a11abe14f2047fa8af48ca0a4ab3a13d76b0d04eb21"
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1797352907);
            result.Prefix.ShouldBe("918c3ec575ae4ba6aecb");
            result.Suffix.ShouldBe("e4800abd75114bf7ac46");
            result.Length.ShouldBe(524325223);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("4d720863722c4721a747a02a4004567b59202135eafa4d149aff19eceaf6ee022279ec3142a04e5dab0d9c38f0224631a474e51bcdae4163ac343057a77f0e93c4a8bf76327042f4a10c2bcf35c892b103c3eb2d8b2f48399af1be16ec587319f86d26a5e0914a048051a11abe14f2047fa8af48ca0a4ab3a13d76b0d04eb21");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                StartNumber = 1952722932,
                Prefix = "923640c8e9564809b5db",
                Suffix = "828de1339baf49c5acd5",
                Length = 1844968732,
                Active = true,
                Description = "baa95ebbba9f4c1baf6e202df2bf6a6cf785caacabe74d67b012f6e97206af503f3f2e94e5cd4b63bdae94c9e1c1459c6988a31ae62b4a7eb40e11b808b77e11b7559ad5f3164612a703302dc0cdc0492dfdd89a038f4f46a3ec091b6fcba1c8245c0fde3cca481aab0af85022210c44a29c0f456ebf4699acb26c60f92f7ee"
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1952722932);
            result.Prefix.ShouldBe("923640c8e9564809b5db");
            result.Suffix.ShouldBe("828de1339baf49c5acd5");
            result.Length.ShouldBe(1844968732);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("baa95ebbba9f4c1baf6e202df2bf6a6cf785caacabe74d67b012f6e97206af503f3f2e94e5cd4b63bdae94c9e1c1459c6988a31ae62b4a7eb40e11b808b77e11b7559ad5f3164612a703302dc0cdc0492dfdd89a038f4f46a3ec091b6fcba1c8245c0fde3cca481aab0af85022210c44a29c0f456ebf4699acb26c60f92f7ee");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"));

            result.ShouldBeNull();
        }
    }
}