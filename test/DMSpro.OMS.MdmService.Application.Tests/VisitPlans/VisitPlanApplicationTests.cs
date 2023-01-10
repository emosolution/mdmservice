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
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3")).ShouldBe(true);
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("473b81d2-fc78-4a5b-ba79-f56cb707d365")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _visitPlansAppService.GetAsync(Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitPlanCreateDto
            {
                DateVisit = new DateTime(2010, 10, 16),
                Distance = 1224741970,
                VisitOrder = 610421816,
                DayOfWeek = default,
                Week = 1346702343,
                Month = 913299697,
                Year = 70702436,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"),

            };

            // Act
            var serviceResult = await _visitPlansAppService.CreateAsync(input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2010, 10, 16));
            result.Distance.ShouldBe(1224741970);
            result.VisitOrder.ShouldBe(610421816);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(1346702343);
            result.Month.ShouldBe(913299697);
            result.Year.ShouldBe(70702436);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitPlanUpdateDto()
            {
                DateVisit = new DateTime(2004, 5, 24),
                Distance = 252795190,
                VisitOrder = 1825226673,
                DayOfWeek = default,
                Week = 244518332,
                Month = 1691579798,
                Year = 2028353756,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"),
                CustomerId = Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"),

            };

            // Act
            var serviceResult = await _visitPlansAppService.UpdateAsync(Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"), input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2004, 5, 24));
            result.Distance.ShouldBe(252795190);
            result.VisitOrder.ShouldBe(1825226673);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(244518332);
            result.Month.ShouldBe(1691579798);
            result.Year.ShouldBe(2028353756);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitPlansAppService.DeleteAsync(Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"));

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == Guid.Parse("bd83721f-3a02-42d0-8187-4e5504558cd3"));

            result.ShouldBeNull();
        }
    }
}