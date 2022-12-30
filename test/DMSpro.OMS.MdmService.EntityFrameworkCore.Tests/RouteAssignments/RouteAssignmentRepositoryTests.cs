using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.RouteAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IRouteAssignmentRepository _routeAssignmentRepository;

        public RouteAssignmentRepositoryTests()
        {
            _routeAssignmentRepository = GetRequiredService<IRouteAssignmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _routeAssignmentRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _routeAssignmentRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}