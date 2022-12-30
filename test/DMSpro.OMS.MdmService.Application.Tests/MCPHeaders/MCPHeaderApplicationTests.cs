using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeadersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IMCPHeadersAppService _mCPHeadersAppService;
        private readonly IRepository<MCPHeader, Guid> _mCPHeaderRepository;

        public MCPHeadersAppServiceTests()
        {
            _mCPHeadersAppService = GetRequiredService<IMCPHeadersAppService>();
            _mCPHeaderRepository = GetRequiredService<IRepository<MCPHeader, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _mCPHeadersAppService.GetListAsync(new GetMCPHeadersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730")).ShouldBe(true);
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("00c6faa0-177a-4143-9ed3-1e27e13d4916")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _mCPHeadersAppService.GetAsync(Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MCPHeaderCreateDto
            {
                Code = "66d4884b89bb4301844f",
                Name = "e709f54a7e",
                EffectiveDate = new DateTime(2018, 2, 9),
                EndDate = new DateTime(2004, 1, 5),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _mCPHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("66d4884b89bb4301844f");
            result.Name.ShouldBe("e709f54a7e");
            result.EffectiveDate.ShouldBe(new DateTime(2018, 2, 9));
            result.EndDate.ShouldBe(new DateTime(2004, 1, 5));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MCPHeaderUpdateDto()
            {
                Code = "269f441caa2c47299f43",
                Name = "dcd820bdf1e84ee9a685886895a2353d59f10d89cf684eb1b7626e043f6255660db1ef7411dd426e842e1f7cb642c15b1e",
                EffectiveDate = new DateTime(2013, 9, 18),
                EndDate = new DateTime(2007, 10, 7),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _mCPHeadersAppService.UpdateAsync(Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"), input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("269f441caa2c47299f43");
            result.Name.ShouldBe("dcd820bdf1e84ee9a685886895a2353d59f10d89cf684eb1b7626e043f6255660db1ef7411dd426e842e1f7cb642c15b1e");
            result.EffectiveDate.ShouldBe(new DateTime(2013, 9, 18));
            result.EndDate.ShouldBe(new DateTime(2007, 10, 7));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _mCPHeadersAppService.DeleteAsync(Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"));

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"));

            result.ShouldBeNull();
        }
    }
}