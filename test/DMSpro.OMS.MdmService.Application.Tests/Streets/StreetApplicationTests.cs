using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IStreetsAppService _streetsAppService;
        private readonly IRepository<Street, Guid> _streetRepository;

        public StreetsAppServiceTests()
        {
            _streetsAppService = GetRequiredService<IStreetsAppService>();
            _streetRepository = GetRequiredService<IRepository<Street, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _streetsAppService.GetListAsync(new GetStreetsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("69e36f74-b7e3-47d7-9f72-ca0b39bdef33")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _streetsAppService.GetAsync(Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new StreetCreateDto
            {
                Name = "2Yj3pjou5"
            };

            // Act
            var serviceResult = await _streetsAppService.CreateAsync(input);

            // Assert
            var result = await _streetRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("2Yj3pjou5");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new StreetUpdateDto()
            {
                Name = "XSsisuugsuyoqs"
            };

            // Act
            var serviceResult = await _streetsAppService.UpdateAsync(Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"), input);

            // Assert
            var result = await _streetRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("XSsisuugsuyoqs");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _streetsAppService.DeleteAsync(Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"));

            // Assert
            var result = await _streetRepository.FindAsync(c => c.Id == Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"));

            result.ShouldBeNull();
        }
    }
}