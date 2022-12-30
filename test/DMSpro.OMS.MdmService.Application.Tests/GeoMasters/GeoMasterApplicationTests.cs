using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMastersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IGeoMastersAppService _geoMastersAppService;
        private readonly IRepository<GeoMaster, Guid> _geoMasterRepository;

        public GeoMastersAppServiceTests()
        {
            _geoMastersAppService = GetRequiredService<IGeoMastersAppService>();
            _geoMasterRepository = GetRequiredService<IRepository<GeoMaster, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _geoMastersAppService.GetListAsync(new GetGeoMastersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.GeoMaster.Id == Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")).ShouldBe(true);
            result.Items.Any(x => x.GeoMaster.Id == Guid.Parse("62bf13ac-74c4-4104-9bd2-98db57fc30e4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _geoMastersAppService.GetAsync(Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new GeoMasterCreateDto
            {
                Code = "ea0a40c55ce94dd887424a86933416cd15fe2c9f1fb942d69e25816e83",
                ERPCode = "e2946d76b3604379b93a47db71196053",
                Name = "d74742e6e9b04142ab65c954c778b6902537803bdb194304b01fcc2ab9a9469dce190b8b9b174c499fc4559643eb4ecb9a32",
                Level = 31756220
            };

            // Act
            var serviceResult = await _geoMastersAppService.CreateAsync(input);

            // Assert
            var result = await _geoMasterRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ea0a40c55ce94dd887424a86933416cd15fe2c9f1fb942d69e25816e83");
            result.ERPCode.ShouldBe("e2946d76b3604379b93a47db71196053");
            result.Name.ShouldBe("d74742e6e9b04142ab65c954c778b6902537803bdb194304b01fcc2ab9a9469dce190b8b9b174c499fc4559643eb4ecb9a32");
            result.Level.ShouldBe(31756220);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new GeoMasterUpdateDto()
            {
                Code = "690aa0ed65bd4464af911b88e89a26b2a1213a060b224",
                ERPCode = "f3042cf49fb44127b20a1b5c298ec7197dd2174b35fb42efa8ecd8c8825c",
                Name = "526299daa0a2422ab69f783c6bbfc26915c4e63b0a7c4032b187c72b4fc8101352d63ace44e44455a11eb0b489e1573eff91",
                Level = 1028055831
            };

            // Act
            var serviceResult = await _geoMastersAppService.UpdateAsync(Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"), input);

            // Assert
            var result = await _geoMasterRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("690aa0ed65bd4464af911b88e89a26b2a1213a060b224");
            result.ERPCode.ShouldBe("f3042cf49fb44127b20a1b5c298ec7197dd2174b35fb42efa8ecd8c8825c");
            result.Name.ShouldBe("526299daa0a2422ab69f783c6bbfc26915c4e63b0a7c4032b187c72b4fc8101352d63ace44e44455a11eb0b489e1573eff91");
            result.Level.ShouldBe(1028055831);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _geoMastersAppService.DeleteAsync(Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"));

            // Assert
            var result = await _geoMasterRepository.FindAsync(c => c.Id == Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"));

            result.ShouldBeNull();
        }
    }
}