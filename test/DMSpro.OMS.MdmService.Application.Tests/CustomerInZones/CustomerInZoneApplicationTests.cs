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
        public async Task GetAsync()
        {
            // Act
            var result = await _customerInZonesAppService.GetAsync(Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerInZoneCreateDto
            {
                EffectiveDate = new DateTime(2019, 10, 12),
                EndDate = new DateTime(2013, 1, 20),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 10, 12));
            result.EndDate.ShouldBe(new DateTime(2013, 1, 20));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2012, 4, 1),
                EndDate = new DateTime(2014, 10, 25),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerInZonesAppService.UpdateAsync(Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"), input);

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2012, 4, 1));
            result.EndDate.ShouldBe(new DateTime(2014, 10, 25));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerInZonesAppService.DeleteAsync(Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"));

            // Assert
            var result = await _customerInZoneRepository.FindAsync(c => c.Id == Guid.Parse("55164995-b1b0-40b1-8070-cbc6b7b2a338"));

            result.ShouldBeNull();
        }
    }
}