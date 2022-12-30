using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IUOMsAppService _uOMsAppService;
        private readonly IRepository<UOM, Guid> _uOMRepository;

        public UOMsAppServiceTests()
        {
            _uOMsAppService = GetRequiredService<IUOMsAppService>();
            _uOMRepository = GetRequiredService<IRepository<UOM, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _uOMsAppService.GetListAsync(new GetUOMsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b6b100a3-92b1-47c9-9005-be037cb23a51")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _uOMsAppService.GetAsync(Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UOMCreateDto
            {
                Code = "b17d09ab2abc4918962f",
                Name = "fc0fae6e5bc946c0a312fe452e9531d16c94957b36b04a5b87fef13d0d52e4012e265808dc884b598bf7159ac4867e10253904df5ba24ebc8d5271e31d187a00e57c9d22354744dc991fab8a6e42041a056818850e44478490207f5acda53ff793059294"
            };

            // Act
            var serviceResult = await _uOMsAppService.CreateAsync(input);

            // Assert
            var result = await _uOMRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("b17d09ab2abc4918962f");
            result.Name.ShouldBe("fc0fae6e5bc946c0a312fe452e9531d16c94957b36b04a5b87fef13d0d52e4012e265808dc884b598bf7159ac4867e10253904df5ba24ebc8d5271e31d187a00e57c9d22354744dc991fab8a6e42041a056818850e44478490207f5acda53ff793059294");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UOMUpdateDto()
            {
                Code = "db740c802ca34f35897e",
                Name = "108a087be3bb42598e7246a6ba6abd396a1b97b23c9946efafa1e0df8b2d6d142ac31fa96ccc4e82b53a4a1600fb381e4490f7435a8b4e8b9633929985f1bd668e3dc141f69c470591975f7a263f31750e1ee45ff13f4da0b8d79dddd418f21de4ad1b8a"
            };

            // Act
            var serviceResult = await _uOMsAppService.UpdateAsync(Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"), input);

            // Assert
            var result = await _uOMRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("db740c802ca34f35897e");
            result.Name.ShouldBe("108a087be3bb42598e7246a6ba6abd396a1b97b23c9946efafa1e0df8b2d6d142ac31fa96ccc4e82b53a4a1600fb381e4490f7435a8b4e8b9633929985f1bd668e3dc141f69c470591975f7a263f31750e1ee45ff13f4da0b8d79dddd418f21de4ad1b8a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _uOMsAppService.DeleteAsync(Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"));

            // Assert
            var result = await _uOMRepository.FindAsync(c => c.Id == Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"));

            result.ShouldBeNull();
        }
    }
}