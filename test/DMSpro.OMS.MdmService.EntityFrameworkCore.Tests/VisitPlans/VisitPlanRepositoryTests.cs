using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlanRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IVisitPlanRepository _visitPlanRepository;

        public VisitPlanRepositoryTests()
        {
            _visitPlanRepository = GetRequiredService<IVisitPlanRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitPlanRepository.GetListAsync(
                    dayOfWeek: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _visitPlanRepository.GetCountAsync(
                    dayOfWeek: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}