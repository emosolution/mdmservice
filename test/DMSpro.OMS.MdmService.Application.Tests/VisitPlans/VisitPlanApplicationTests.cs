using System;
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
        public async Task GetAsync()
        {
            // Act
            var result = await _visitPlansAppService.GetAsync(Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitPlanCreateDto
            {
                DateVisit = new DateTime(2012, 10, 21),
                Distance = 147673651,
                VisitOrder = 1699051720,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                RouteId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
            };

            // Act
            var serviceResult = await _visitPlansAppService.CreateAsync(input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2012, 10, 21));
            result.Distance.ShouldBe(147673651);
            result.VisitOrder.ShouldBe(1699051720);
            result.DayOfWeek.ShouldBe(DayOfWeek.Sunday);
            result.Week.ShouldBe(42);
            result.Month.ShouldBe(10);
            result.Year.ShouldBe(2012);
            result.IsCommando.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitPlanUpdateDto()
            {
                DateVisit = new DateTime(2020, 8, 8),
                Distance = 2107764423,
                VisitOrder = 124823590,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                RouteId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),

            };

            // Act
            var serviceResult = await _visitPlansAppService.UpdateAsync(Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"), input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2020, 8, 8));
            result.Distance.ShouldBe(2107764423);
            result.VisitOrder.ShouldBe(124823590);
            result.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
            result.Week.ShouldBe(32);
            result.Month.ShouldBe(8);
            result.Year.ShouldBe(2012);
            result.IsCommando.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitPlansAppService.DeleteAsync(Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"));

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == Guid.Parse("1280a097-e2bb-4283-bc5e-37d343c44f58"));

            result.ShouldBeNull();
        }
    }
}