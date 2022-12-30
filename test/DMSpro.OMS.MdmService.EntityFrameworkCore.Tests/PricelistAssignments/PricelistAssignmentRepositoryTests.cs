using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;

        public PricelistAssignmentRepositoryTests()
        {
            _pricelistAssignmentRepository = GetRequiredService<IPricelistAssignmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _pricelistAssignmentRepository.GetListAsync(
                    description: "572121251a7540978bcfdf96956dea791b5fdef28f0244b790e5f115073250bf"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _pricelistAssignmentRepository.GetCountAsync(
                    description: "4ef22585ae7c423aac290324c6550a06f49170e913e94129b167fcbf9208114f5d9f"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}