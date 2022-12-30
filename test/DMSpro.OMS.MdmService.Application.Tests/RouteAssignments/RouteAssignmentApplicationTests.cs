using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IRouteAssignmentsAppService _routeAssignmentsAppService;
        private readonly IRepository<RouteAssignment, Guid> _routeAssignmentRepository;

        public RouteAssignmentsAppServiceTests()
        {
            _routeAssignmentsAppService = GetRequiredService<IRouteAssignmentsAppService>();
            _routeAssignmentRepository = GetRequiredService<IRepository<RouteAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _routeAssignmentsAppService.GetListAsync(new GetRouteAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.RouteAssignment.Id == Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd")).ShouldBe(true);
            result.Items.Any(x => x.RouteAssignment.Id == Guid.Parse("837dc18d-5124-4243-9494-c355fefaa929")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _routeAssignmentsAppService.GetAsync(Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new RouteAssignmentCreateDto
            {
                EffectiveDate = new DateTime(2013, 9, 4),
                EndDate = new DateTime(2014, 9, 27),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _routeAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _routeAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2013, 9, 4));
            result.EndDate.ShouldBe(new DateTime(2014, 9, 27));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new RouteAssignmentUpdateDto()
            {
                EffectiveDate = new DateTime(2008, 8, 9),
                EndDate = new DateTime(2015, 11, 12),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _routeAssignmentsAppService.UpdateAsync(Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"), input);

            // Assert
            var result = await _routeAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 8, 9));
            result.EndDate.ShouldBe(new DateTime(2015, 11, 12));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _routeAssignmentsAppService.DeleteAsync(Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"));

            // Assert
            var result = await _routeAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("794f8ec0-a524-4a91-bd38-595b2d27d7dd"));

            result.ShouldBeNull();
        }
    }
}