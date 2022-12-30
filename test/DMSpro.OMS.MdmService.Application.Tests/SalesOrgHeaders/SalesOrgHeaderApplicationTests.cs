using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeadersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesOrgHeadersAppService _salesOrgHeadersAppService;
        private readonly IRepository<SalesOrgHeader, Guid> _salesOrgHeaderRepository;

        public SalesOrgHeadersAppServiceTests()
        {
            _salesOrgHeadersAppService = GetRequiredService<ISalesOrgHeadersAppService>();
            _salesOrgHeaderRepository = GetRequiredService<IRepository<SalesOrgHeader, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesOrgHeadersAppService.GetListAsync(new GetSalesOrgHeadersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("ba3324d2-dfa8-40fa-99da-4b4207f1f6ad")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgHeadersAppService.GetAsync(Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderCreateDto
            {
                Code = "6d8470e8618345eb8e04",
                Name = "63fec784cfbe427aa2c5b1e078c",
                Active = true
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("6d8470e8618345eb8e04");
            result.Name.ShouldBe("63fec784cfbe427aa2c5b1e078c");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderUpdateDto()
            {
                Code = "9430577e832b4155988a",
                Name = "d3e495cd14c04227899833cebe82ac1c90d6f7322c954d5dbe707278415ca2",
                Active = true
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.UpdateAsync(Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"), input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("9430577e832b4155988a");
            result.Name.ShouldBe("d3e495cd14c04227899833cebe82ac1c90d6f7322c954d5dbe707278415ca2");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgHeadersAppService.DeleteAsync(Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"));

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"));

            result.ShouldBeNull();
        }
    }
}