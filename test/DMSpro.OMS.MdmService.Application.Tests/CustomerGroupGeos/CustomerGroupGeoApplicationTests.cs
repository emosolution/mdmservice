using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeosAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;
        private readonly IRepository<CustomerGroupGeo, Guid> _customerGroupGeoRepository;

        public CustomerGroupGeosAppServiceTests()
        {
            _customerGroupGeosAppService = GetRequiredService<ICustomerGroupGeosAppService>();
            _customerGroupGeoRepository = GetRequiredService<IRepository<CustomerGroupGeo, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupGeosAppService.GetAsync(Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupGeoCreateDto
            {
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                GeoMaster0Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),

            };

            // Act
            var serviceResult = await _customerGroupGeosAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe(null);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupGeoUpdateDto()
            {
                GeoMaster0Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),

            };

            // Act
            var serviceResult = await _customerGroupGeosAppService.UpdateAsync(Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"), input);

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe(null);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupGeosAppService.DeleteAsync(Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"));

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == Guid.Parse("c9aaecb7-1e9f-4aeb-a020-dee128c79ac8"));

            result.ShouldBeNull();
        }
    }
}