using System;
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
        public async Task GetAsync()
        {
            // Act
            var result = await _pricelistAssignmentsAppService.GetAsync(Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentCreateDto
            {
                PriceListId = Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe(PricelistAssignmentConsts.DefaultDescription);
            result.ReleasedDate.ShouldBe(null);
            result.IsReleased.ShouldBe(false);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PricelistAssignmentUpdateDto()
            {
                Description = "817b5675fc19441bad0431f6fc1a964f44bc1a9b21df4b9ab37c22cf7a84d89519da94aa89f84467a014146d346f6f6b4f5be66fb48a4ccab501fc75830a58c08a289b7735c64689834101aedb9313209461f5d01e2c4ea6ba6cf9d300388cc8d1b94636ed3b4886bf700e1312a0fa21135e0c956a8448d5b14ce51ea483f4d0fd1073d8cfb2490299168ed19f41b6a5cd4e8ae97b1b488eb9d1bf3c77fbb014d828c0c6e73f42e491161e3cc2c03cba610a39588646446cac9afbbd9f3e30e06528c0c3559b465abc36bfc010eae47b087d296cb0884a67a02025710d14512bd71536779f8f428cac7605aca512c8afda86917404ca4001a1cb",
                PriceListId = Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _pricelistAssignmentsAppService.UpdateAsync(Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"), input);

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("817b5675fc19441bad0431f6fc1a964f44bc1a9b21df4b9ab37c22cf7a84d89519da94aa89f84467a014146d346f6f6b4f5be66fb48a4ccab501fc75830a58c08a289b7735c64689834101aedb9313209461f5d01e2c4ea6ba6cf9d300388cc8d1b94636ed3b4886bf700e1312a0fa21135e0c956a8448d5b14ce51ea483f4d0fd1073d8cfb2490299168ed19f41b6a5cd4e8ae97b1b488eb9d1bf3c77fbb014d828c0c6e73f42e491161e3cc2c03cba610a39588646446cac9afbbd9f3e30e06528c0c3559b465abc36bfc010eae47b087d296cb0884a67a02025710d14512bd71536779f8f428cac7605aca512c8afda86917404ca4001a1cb");
            result.ReleasedDate.ShouldBe(new DateTime(2004, 4, 16));
            result.IsReleased.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _pricelistAssignmentsAppService.DeleteAsync(Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"));

            // Assert
            var result = await _pricelistAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("a45bc2f2-49d4-43aa-9b37-6a4c88e2721c"));

            result.ShouldBeNull();
        }
    }
}