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
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgHeadersAppService.GetAsync(Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderCreateDto
            {
                Name = "94c42f71780444959f8025a1e8e9e90e91632c985419404290d65d1c36a7db75d32ec3f09fef4263bb25a984fc649ca5f42bb66873d148069c7e73f501d2d2ea77a6e126443843509674cd9fd95cb685680eb0f0e10146a288e522194c1ba56d2dcbb252a46f47bf88b2127d7817044fca9b2d9b09cb45fd92ca8e111837cff",
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("94c42f71780444959f8025a1e8e9e90e91632c985419404290d65d1c36a7db75d32ec3f09fef4263bb25a984fc649ca5f42bb66873d148069c7e73f501d2d2ea77a6e126443843509674cd9fd95cb685680eb0f0e10146a288e522194c1ba56d2dcbb252a46f47bf88b2127d7817044fca9b2d9b09cb45fd92ca8e111837cff");
            result.Active.ShouldBe(true);
            result.Status.ShouldBe(Status.OPEN);
        }
    }
}