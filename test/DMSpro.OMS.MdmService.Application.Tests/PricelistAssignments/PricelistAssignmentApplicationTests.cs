using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPricelistAssignmentsAppService _pricelistAssignmentsAppService;
        private readonly IRepository<PricelistAssignment, Guid> _pricelistAssignmentRepository;

        public PricelistAssignmentsAppServiceTests()
        {
            _pricelistAssignmentsAppService = GetRequiredService<IPricelistAssignmentsAppService>();
            _pricelistAssignmentRepository = GetRequiredService<IRepository<PricelistAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetListAsync(new GetPricelistAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PricelistAssignment.Id == Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d")).ShouldBe(true);
            result.Items.Any(x => x.PricelistAssignment.Id == Guid.Parse("200a0319-42e3-49bc-9b7d-699ed6a8304b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetAsync(Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentCreateDto
            {
                Description = "83de8e9dc96e4502be8b8f2d6b2ee84d0053298825ff49d0b9bae232e274",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                CustomerGroupId = Guid.Parse("df6f7159-5418-4582-80bc-f56cf232a8b6")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("83de8e9dc96e4502be8b8f2d6b2ee84d0053298825ff49d0b9bae232e274");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentUpdateDto()
            {
                Description = "8d00446cf6614db38f6ebee609638824849ea5e3c9d341abb7d9a3f",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                CustomerGroupId = Guid.Parse("df6f7159-5418-4582-80bc-f56cf232a8b6")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.UpdateAsync(Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"), input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("8d00446cf6614db38f6ebee609638824849ea5e3c9d341abb7d9a3f");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _pricelistAssignmentsAppService.DeleteAsync(Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"));

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"));

            result.ShouldBeNull();
        }
    }
}