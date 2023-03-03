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
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("c5560f29-7b0d-4263-82dc-fa7c1f48f2c4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                StartNumber = 1486911631,
                Prefix = "cfe3267b62d7432e889b",
                Suffix = "fd65db97e58244ae87ab",
                Length = 1193454886
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1486911631);
            result.Prefix.ShouldBe("cfe3267b62d7432e889b");
            result.Suffix.ShouldBe("fd65db97e58244ae87ab");
            result.Length.ShouldBe(1193454886);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                StartNumber = 754431018,
                Prefix = "cb354aaf23df40b380f1",
                Suffix = "95bace5d541c4b8baa97",
                Length = 88975725
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(754431018);
            result.Prefix.ShouldBe("cb354aaf23df40b380f1");
            result.Suffix.ShouldBe("95bace5d541c4b8baa97");
            result.Length.ShouldBe(88975725);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"));

            result.ShouldBeNull();
        }
    }
}