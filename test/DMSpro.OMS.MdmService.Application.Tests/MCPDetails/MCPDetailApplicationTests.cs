using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IMCPDetailsAppService _mCPDetailsAppService;
        private readonly IRepository<MCPDetail, Guid> _mCPDetailRepository;

        public MCPDetailsAppServiceTests()
        {
            _mCPDetailsAppService = GetRequiredService<IMCPDetailsAppService>();
            _mCPDetailRepository = GetRequiredService<IRepository<MCPDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _mCPDetailsAppService.GetListAsync(new GetMCPDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.MCPDetail.Id == Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9")).ShouldBe(true);
            result.Items.Any(x => x.MCPDetail.Id == Guid.Parse("4bf4fc22-2402-4854-a495-0df8c26ae80c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _mCPDetailsAppService.GetAsync(Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MCPDetailCreateDto
            {
                Code = "16ea7ba9d4ec476c870d",
                EffectiveDate = new DateTime(2006, 1, 15),
                EndDate = new DateTime(2004, 8, 11),
                Distance = 1465954038,
                VisitOrder = 1952857112,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true,
                Week1 = true,
                Week2 = true,
                Week3 = true,
                Week4 = true,
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781"),
                MCPHeaderId = Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730")
            };

            // Act
            var serviceResult = await _mCPDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _mCPDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("16ea7ba9d4ec476c870d");
            result.EffectiveDate.ShouldBe(new DateTime(2006, 1, 15));
            result.EndDate.ShouldBe(new DateTime(2004, 8, 11));
            result.Distance.ShouldBe(1465954038);
            result.VisitOrder.ShouldBe(1952857112);
            result.Monday.ShouldBe(true);
            result.Tuesday.ShouldBe(true);
            result.Wednesday.ShouldBe(true);
            result.Thursday.ShouldBe(true);
            result.Friday.ShouldBe(true);
            result.Saturday.ShouldBe(true);
            result.Sunday.ShouldBe(true);
            result.Week1.ShouldBe(true);
            result.Week2.ShouldBe(true);
            result.Week3.ShouldBe(true);
            result.Week4.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MCPDetailUpdateDto()
            {
                Code = "bf471325a9204d909ac6",
                EffectiveDate = new DateTime(2011, 6, 11),
                EndDate = new DateTime(2019, 8, 1),
                Distance = 1117645876,
                VisitOrder = 1598542561,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true,
                Week1 = true,
                Week2 = true,
                Week3 = true,
                Week4 = true,
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781"),
                MCPHeaderId = Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730")
            };

            // Act
            var serviceResult = await _mCPDetailsAppService.UpdateAsync(Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"), input);

            // Assert
            var result = await _mCPDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("bf471325a9204d909ac6");
            result.EffectiveDate.ShouldBe(new DateTime(2011, 6, 11));
            result.EndDate.ShouldBe(new DateTime(2019, 8, 1));
            result.Distance.ShouldBe(1117645876);
            result.VisitOrder.ShouldBe(1598542561);
            result.Monday.ShouldBe(true);
            result.Tuesday.ShouldBe(true);
            result.Wednesday.ShouldBe(true);
            result.Thursday.ShouldBe(true);
            result.Friday.ShouldBe(true);
            result.Saturday.ShouldBe(true);
            result.Sunday.ShouldBe(true);
            result.Week1.ShouldBe(true);
            result.Week2.ShouldBe(true);
            result.Week3.ShouldBe(true);
            result.Week4.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _mCPDetailsAppService.DeleteAsync(Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"));

            // Assert
            var result = await _mCPDetailRepository.FindAsync(c => c.Id == Guid.Parse("40eb1f6c-27a8-44c6-91e5-5c40816e7ab9"));

            result.ShouldBeNull();
        }
    }
}