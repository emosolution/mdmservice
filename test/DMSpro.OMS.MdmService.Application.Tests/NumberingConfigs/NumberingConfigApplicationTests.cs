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
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("ed8dc997-c9c1-4312-80a6-3f858ade8e23")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                StartNumber = 892980332,
                Prefix = "2bc4d1c1b291437393fe",
                Suffix = "7d4e12c76e6f4b189ac9",
                Length = 1322091069,
                Active = true
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(892980332);
            result.Prefix.ShouldBe("2bc4d1c1b291437393fe");
            result.Suffix.ShouldBe("7d4e12c76e6f4b189ac9");
            result.Length.ShouldBe(1322091069);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                StartNumber = 621049075,
                Prefix = "a4db362702574521a336",
                Suffix = "a6bfac8c9eb540b98dc8",
                Length = 511842015,
                Active = true
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(621049075);
            result.Prefix.ShouldBe("a4db362702574521a336");
            result.Suffix.ShouldBe("a6bfac8c9eb540b98dc8");
            result.Length.ShouldBe(511842015);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"));

            result.ShouldBeNull();
        }
    }
}