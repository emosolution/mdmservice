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
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5")).ShouldBe(true);
            result.Items.Any(x => x.VisitPlan.Id == Guid.Parse("fd5dcdb3-5c17-4d42-97b0-af0437e6b5f0")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _visitPlansAppService.GetAsync(Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitPlanCreateDto
            {
                DateVisit = new DateTime(2020, 4, 8),
                Distance = 840561331,
                VisitOrder = 1531089305,
                DayOfWeek = default,
                Week = 1269643596,
                Month = 534468423,
                Year = 1933672059,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9")
            };

            // Act
            var serviceResult = await _visitPlansAppService.CreateAsync(input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2020, 4, 8));
            result.Distance.ShouldBe(840561331);
            result.VisitOrder.ShouldBe(1531089305);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(1269643596);
            result.Month.ShouldBe(534468423);
            result.Year.ShouldBe(1933672059);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitPlanUpdateDto()
            {
                DateVisit = new DateTime(2007, 5, 15),
                Distance = 497187821,
                VisitOrder = 690384115,
                DayOfWeek = default,
                Week = 2106656363,
                Month = 253959642,
                Year = 1864723990,
                MCPDetailId = Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9")
            };

            // Act
            var serviceResult = await _visitPlansAppService.UpdateAsync(Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"), input);

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DateVisit.ShouldBe(new DateTime(2007, 5, 15));
            result.Distance.ShouldBe(497187821);
            result.VisitOrder.ShouldBe(690384115);
            result.DayOfWeek.ShouldBe(default);
            result.Week.ShouldBe(2106656363);
            result.Month.ShouldBe(253959642);
            result.Year.ShouldBe(1864723990);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitPlansAppService.DeleteAsync(Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"));

            // Assert
            var result = await _visitPlanRepository.FindAsync(c => c.Id == Guid.Parse("0953a5bf-8ec9-4ce7-beba-1af369be39e5"));

            result.ShouldBeNull();
        }
    }
}