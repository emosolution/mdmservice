using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public class VisitPlansAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IVisitPlansAppService _visitPlansAppService;
        private readonly IRepository<VisitPlan, Guid> _visitPlanRepository;

        public VisitPlansAppServiceTests()
        {
            _visitPlansAppService = GetRequiredService<IVisitPlansAppService>();
            _visitPlanRepository = GetRequiredService<IRepository<VisitPlan, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _visitPlansAppService.GetListAsync(new GetVisitPlansInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400")).ShouldBe(true);
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("56b0c461-ef96-495e-8a92-2f7fc7779da8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _visitPlansAppService.GetAsync(Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitPlanCreateDto
            {
                DateVisit = new DateTime(2000, 2, 16),
                Distance = 1218038870,
                VisitOrder = 1475442832,
                DayOfWeek = default,
                Week = 1661194589,
                Month = 671464393,
                Year = 1451210064,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _visitPlansAppService.CreateAsync(input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2000, 2, 16));
            result.Distance.ShouldBe(1218038870);
            result.VisitOrder.ShouldBe(1475442832);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(1661194589);
            result.Month.ShouldBe(671464393);
            result.Year.ShouldBe(1451210064);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitPlanUpdateDto()
            {
                DateVisit = new DateTime(2002, 3, 27),
                Distance = 1338196626,
                VisitOrder = 1856076201,
                DayOfWeek = default,
                Week = 1060723367,
                Month = 524301992,
                Year = 1256278073,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _visitPlansAppService.UpdateAsync(Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"), input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2002, 3, 27));
            result.Distance.ShouldBe(1338196626);
            result.VisitOrder.ShouldBe(1856076201);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(1060723367);
            result.Month.ShouldBe(524301992);
            result.Year.ShouldBe(1256278073);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitPlansAppService.DeleteAsync(Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"));

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == Guid.Parse("c4656915-1ccb-4eb8-adad-e074abebc400"));

            result.ShouldBeNull();
        }
    }
}