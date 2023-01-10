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
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc")).ShouldBe(true);
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("eed14627-87a5-4c56-b5eb-569558c73523")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _mCPHeadersAppService.GetAsync(Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MCPHeaderCreateDto
            {
                Code = "1279c1d7f7464cf78e5e",
                Name = "4656c95a48cd4f43ba",
                EffectiveDate = new DateTime(2000, 2, 8),
                EndDate = new DateTime(2020, 11, 7),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),

            };

            // Act
            var serviceResult = await _mCPHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("1279c1d7f7464cf78e5e");
            result.Name.ShouldBe("4656c95a48cd4f43ba");
            result.EffectiveDate.ShouldBe(new DateTime(2000, 2, 8));
            result.EndDate.ShouldBe(new DateTime(2020, 11, 7));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MCPHeaderUpdateDto()
            {
                Code = "70c4923563414c9c99e3",
                Name = "cbc935f958a84a608f163d3629dcf4468b45030c53714a889c7e4ca4c",
                EffectiveDate = new DateTime(2011, 6, 23),
                EndDate = new DateTime(2022, 2, 8),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),

            };

            // Act
            var serviceResult = await _mCPHeadersAppService.UpdateAsync(Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"), input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("70c4923563414c9c99e3");
            result.Name.ShouldBe("cbc935f958a84a608f163d3629dcf4468b45030c53714a889c7e4ca4c");
            result.EffectiveDate.ShouldBe(new DateTime(2011, 6, 23));
            result.EndDate.ShouldBe(new DateTime(2022, 2, 8));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _mCPHeadersAppService.DeleteAsync(Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"));

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"));

            result.ShouldBeNull();
        }
    }
}