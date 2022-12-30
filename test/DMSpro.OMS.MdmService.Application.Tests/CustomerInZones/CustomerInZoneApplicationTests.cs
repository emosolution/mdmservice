using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZonesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerInZonesAppService _customerInZonesAppService;
        private readonly IRepository<CustomerInZone, Guid> _customerInZoneRepository;

        public CustomerInZonesAppServiceTests()
        {
            _customerInZonesAppService = GetRequiredService<ICustomerInZonesAppService>();
            _customerInZoneRepository = GetRequiredService<IRepository<CustomerInZone, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerInZonesAppService.GetListAsync(new GetCustomerInZonesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerInZone.Id == Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39")).ShouldBe(true);
            result.Items.Any(x => x.CustomerInZone.Id == Guid.Parse("c5053680-b579-49de-8cbe-a30adc7fed0d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerInZonesAppService.GetAsync(Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerInZoneCreateDto
            {
                EffectiveDate = new DateTime(2011, 1, 20),
                EndDate = new DateTime(2011, 1, 22),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2011, 1, 20));
            result.EndDate.ShouldBe(new DateTime(2011, 1, 22));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2019, 8, 13),
                EndDate = new DateTime(2007, 10, 21),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerInZonesAppService.UpdateAsync(Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"), input);

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 8, 13));
            result.EndDate.ShouldBe(new DateTime(2007, 10, 21));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerInZonesAppService.DeleteAsync(Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"));

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == Guid.Parse("ece3682b-9037-4262-89fc-3193b52afd39"));

            result.ShouldBeNull();
        }
    }
}