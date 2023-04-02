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
                Code = "ee32bed3b9ba4361afdc",
                Name = "94c42f71780444959f8025a1e8e9e90e91632c985419404290d65d1c36a7db75d32ec3f09fef4263bb25a984fc649ca5f42bb66873d148069c7e73f501d2d2ea77a6e126443843509674cd9fd95cb685680eb0f0e10146a288e522194c1ba56d2dcbb252a46f47bf88b2127d7817044fca9b2d9b09cb45fd92ca8e111837cff",
                Active = true,
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ee32bed3b9ba4361afdc");
            result.Name.ShouldBe("94c42f71780444959f8025a1e8e9e90e91632c985419404290d65d1c36a7db75d32ec3f09fef4263bb25a984fc649ca5f42bb66873d148069c7e73f501d2d2ea77a6e126443843509674cd9fd95cb685680eb0f0e10146a288e522194c1ba56d2dcbb252a46f47bf88b2127d7817044fca9b2d9b09cb45fd92ca8e111837cff");
            result.Active.ShouldBe(true);
            result.Status.ShouldBe(Status.Open);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderUpdateDto()
            {
                Code = "6300b505d406456488a0",
                Name = "ad6b8af2cc3649aaba84d772281c49381c29e6cf68fd4e9fadbe369b48fc9ce306a7848360584cf2b69439245cdfc69f814a6ea709d1462e9c30a5a89267189c2d65020038204c70a954ba6d701463512af0823dcd224f4480ac50870c62d2f09ab54f6bfb5d44b7bfadd702c323bf84f34247980a294fc3996b11ff0b0200c",
                Active = true,
                Status = default
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.UpdateAsync(Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"), input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("6300b505d406456488a0");
            result.Name.ShouldBe("ad6b8af2cc3649aaba84d772281c49381c29e6cf68fd4e9fadbe369b48fc9ce306a7848360584cf2b69439245cdfc69f814a6ea709d1462e9c30a5a89267189c2d65020038204c70a954ba6d701463512af0823dcd224f4480ac50870c62d2f09ab54f6bfb5d44b7bfadd702c323bf84f34247980a294fc3996b11ff0b0200c");
            result.Active.ShouldBe(true);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgHeadersAppService.DeleteAsync(Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"));

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == Guid.Parse("2d65cdf9-4242-44e8-8583-a57e4f3ee7f7"));

            result.ShouldBeNull();
        }
    }
}