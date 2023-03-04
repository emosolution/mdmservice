using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeosAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupByGeosAppService _customerGroupByGeosAppService;
        private readonly IRepository<CustomerGroupByGeo, Guid> _customerGroupByGeoRepository;

        public CustomerGroupByGeosAppServiceTests()
        {
            _customerGroupByGeosAppService = GetRequiredService<ICustomerGroupByGeosAppService>();
            _customerGroupByGeoRepository = GetRequiredService<IRepository<CustomerGroupByGeo, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupByGeosAppService.GetListAsync(new GetCustomerGroupByGeosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupByGeo.Id == Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupByGeo.Id == Guid.Parse("cb2f83d4-2dbd-4947-b02b-bf67d993b48e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupByGeosAppService.GetAsync(Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupByGeoCreateDto
            {
                Active = true,
                EffectiveDate = new DateTime(2018, 6, 19),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),

            };

            // Act
            var serviceResult = await _customerGroupByGeosAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2018, 6, 19));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupByGeoUpdateDto()
            {
                Active = true,
                EffectiveDate = new DateTime(2001, 6, 16),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),

            };

            // Act
            var serviceResult = await _customerGroupByGeosAppService.UpdateAsync(Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"), input);

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2001, 6, 16));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupByGeosAppService.DeleteAsync(Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"));

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == Guid.Parse("d80a2a0d-fbc9-43f0-8b13-f0ca571774ef"));

            result.ShouldBeNull();
        }
    }
}