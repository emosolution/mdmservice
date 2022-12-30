using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RoutesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IRoutesAppService _routesAppService;
        private readonly IRepository<Route, Guid> _routeRepository;

        public RoutesAppServiceTests()
        {
            _routesAppService = GetRequiredService<IRoutesAppService>();
            _routeRepository = GetRequiredService<IRepository<Route, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _routesAppService.GetListAsync(new GetRoutesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Route.Id == Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61")).ShouldBe(true);
            result.Items.Any(x => x.Route.Id == Guid.Parse("2f432da4-633a-4872-a1cf-1b23f6f923af")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _routesAppService.GetAsync(Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new RouteCreateDto
            {
                CheckIn = true,
                CheckOut = true,
                GPSLock = true,
                OutRoute = true,
                RouteTypeId = Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5")
            };

            // Act
            var serviceResult = await _routesAppService.CreateAsync(input);

            // Assert
            var result = await _routeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CheckIn.ShouldBe(true);
            result.CheckOut.ShouldBe(true);
            result.GPSLock.ShouldBe(true);
            result.OutRoute.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new RouteUpdateDto()
            {
                CheckIn = true,
                CheckOut = true,
                GPSLock = true,
                OutRoute = true,
                RouteTypeId = Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5")
            };

            // Act
            var serviceResult = await _routesAppService.UpdateAsync(Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"), input);

            // Assert
            var result = await _routeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CheckIn.ShouldBe(true);
            result.CheckOut.ShouldBe(true);
            result.GPSLock.ShouldBe(true);
            result.OutRoute.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _routesAppService.DeleteAsync(Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"));

            // Assert
            var result = await _routeRepository.FindAsync(c => c.Id == Guid.Parse("79d7c3ca-a497-49a8-8f14-3ee644bebb61"));

            result.ShouldBeNull();
        }
    }
}