using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Routes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IRouteRepository _routeRepository;

        public RouteRepositoryTests()
        {
            _routeRepository = GetRequiredService<IRouteRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _routeRepository.GetListAsync(
                    checkIn: true,
                    checkOut: true,
                    gpsLock: true,
                    outRoute: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _routeRepository.GetCountAsync(
                    checkIn: true,
                    checkOut: true,
                    gpsLock: true,
                    outRoute: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}