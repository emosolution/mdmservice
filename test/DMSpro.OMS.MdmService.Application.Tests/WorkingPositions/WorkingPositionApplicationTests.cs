using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IWorkingPositionsAppService _workingPositionsAppService;
        private readonly IRepository<WorkingPosition, Guid> _workingPositionRepository;

        public WorkingPositionsAppServiceTests()
        {
            _workingPositionsAppService = GetRequiredService<IWorkingPositionsAppService>();
            _workingPositionRepository = GetRequiredService<IRepository<WorkingPosition, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _workingPositionsAppService.GetListAsync(new GetWorkingPositionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("25e2ff5f-c900-44f8-9b22-b2d338c1b632")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _workingPositionsAppService.GetAsync(Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WorkingPositionCreateDto
            {
                Code = "96ad0b5c1f02464da69d",
                Name = "c9fe2ccf92254e4db1f108e4aba2901462f4f4291",
                Description = "bc02cc49e86d45b3813",
                Active = true
            };

            // Act
            var serviceResult = await _workingPositionsAppService.CreateAsync(input);

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("96ad0b5c1f02464da69d");
            result.Name.ShouldBe("c9fe2ccf92254e4db1f108e4aba2901462f4f4291");
            result.Description.ShouldBe("bc02cc49e86d45b3813");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WorkingPositionUpdateDto()
            {
                Code = "73b34d19331b454faf08",
                Name = "31d5e49e6f5746d684a4b5f0f4c024c4aadcabe6792a41e0832f263a6e60d6c1895d72ba33064057af17f6ca",
                Description = "23febb6a20da443ea0bf27046720c8d5e88cd",
                Active = true
            };

            // Act
            var serviceResult = await _workingPositionsAppService.UpdateAsync(Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"), input);

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("73b34d19331b454faf08");
            result.Name.ShouldBe("31d5e49e6f5746d684a4b5f0f4c024c4aadcabe6792a41e0832f263a6e60d6c1895d72ba33064057af17f6ca");
            result.Description.ShouldBe("23febb6a20da443ea0bf27046720c8d5e88cd");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _workingPositionsAppService.DeleteAsync(Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"));

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == Guid.Parse("f7942152-dbeb-42ad-9935-c3064738dc4e"));

            result.ShouldBeNull();
        }
    }
}