using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDatasAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISystemDatasAppService _systemDatasAppService;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;

        public SystemDatasAppServiceTests()
        {
            _systemDatasAppService = GetRequiredService<ISystemDatasAppService>();
            _systemDataRepository = GetRequiredService<IRepository<SystemData, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _systemDatasAppService.GetListAsync(new GetSystemDatasInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("18db414e-24df-47a9-b033-e96bac671dfb")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _systemDatasAppService.GetAsync(Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SystemDataCreateDto
            {
                Code = "7c99b2a243",
                ValueCode = "4e375fd38b",
                ValueName = "4e1778c665a64f39bfcd61bb18c0390a1902f07da246456ab2972a6145fbc1273be39484139e4184a5ec70bdeb017b20f4f6"
            };

            // Act
            var serviceResult = await _systemDatasAppService.CreateAsync(input);

            // Assert
            var result = await _systemDataRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7c99b2a243");
            result.ValueCode.ShouldBe("4e375fd38b");
            result.ValueName.ShouldBe("4e1778c665a64f39bfcd61bb18c0390a1902f07da246456ab2972a6145fbc1273be39484139e4184a5ec70bdeb017b20f4f6");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SystemDataUpdateDto()
            {
                Code = "ebb5c59033",
                ValueCode = "32c86625a4",
                ValueName = "72f3f6bcf5fe4aedb356270aa32b87f88dd6802c6d654e59be4890ea1c3756e58e67d160e4a24439867f6fea40e35969d61b"
            };

            // Act
            var serviceResult = await _systemDatasAppService.UpdateAsync(Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"), input);

            // Assert
            var result = await _systemDataRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ebb5c59033");
            result.ValueCode.ShouldBe("32c86625a4");
            result.ValueName.ShouldBe("72f3f6bcf5fe4aedb356270aa32b87f88dd6802c6d654e59be4890ea1c3756e58e67d160e4a24439867f6fea40e35969d61b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _systemDatasAppService.DeleteAsync(Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"));

            // Assert
            var result = await _systemDataRepository.FindAsync(c => c.Id == Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"));

            result.ShouldBeNull();
        }
    }
}