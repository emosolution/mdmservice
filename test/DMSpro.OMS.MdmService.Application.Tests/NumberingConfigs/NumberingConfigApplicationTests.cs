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
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("e4cff639-7b5a-4b93-9419-ac80032fdbcd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                StartNumber = 976924717,
                Prefix = "dc59a03ba6664026850b9c04a425bad1f02edc96f0fc43cd854024eb42ebcb0d7008d9267c",
                Suffix = "dc70d5f9ebf64aca900ca4fd5dddba2",
                Length = 373383003
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(976924717);
            result.Prefix.ShouldBe("dc59a03ba6664026850b9c04a425bad1f02edc96f0fc43cd854024eb42ebcb0d7008d9267c");
            result.Suffix.ShouldBe("dc70d5f9ebf64aca900ca4fd5dddba2");
            result.Length.ShouldBe(373383003);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                StartNumber = 1801380096,
                Prefix = "7ff59ae815584174bed70e876d191d2d66d2fcfb1f2d47878b26c7ad7e74af9858efbd91d6a",
                Suffix = "36e1393986a84bfaa990b3",
                Length = 22373851
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1801380096);
            result.Prefix.ShouldBe("7ff59ae815584174bed70e876d191d2d66d2fcfb1f2d47878b26c7ad7e74af9858efbd91d6a");
            result.Suffix.ShouldBe("36e1393986a84bfaa990b3");
            result.Length.ShouldBe(22373851);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"));

            result.ShouldBeNull();
        }
    }
}