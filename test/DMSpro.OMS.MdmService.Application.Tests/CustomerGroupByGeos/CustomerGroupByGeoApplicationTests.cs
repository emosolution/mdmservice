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
            result.Items.Any(x => x.CustomerGroupByGeo.Id == Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupByGeo.Id == Guid.Parse("eef99443-cea3-4efc-851c-935a9e06531f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupByGeosAppService.GetAsync(Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupByGeoCreateDto
            {
                Active = true,
                EffectiveDate = new DateTime(2006, 8, 21),
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                GeoMasterId = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            };

            // Act
            var serviceResult = await _customerGroupByGeosAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2006, 8, 21));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupByGeoUpdateDto()
            {
                Active = true,
                EffectiveDate = new DateTime(2003, 11, 15),
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                GeoMasterId = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            };

            // Act
            var serviceResult = await _customerGroupByGeosAppService.UpdateAsync(Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"), input);

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2003, 11, 15));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupByGeosAppService.DeleteAsync(Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"));

            // Assert
            var result = await _customerGroupByGeoRepository.FindAsync(c => c.Id == Guid.Parse("d647c0d8-11d5-45ac-a94a-26b92b3db71a"));

            result.ShouldBeNull();
        }
    }
}