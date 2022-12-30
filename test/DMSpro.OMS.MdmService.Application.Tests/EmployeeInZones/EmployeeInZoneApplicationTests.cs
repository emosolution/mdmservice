using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZonesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IEmployeeInZonesAppService _employeeInZonesAppService;
        private readonly IRepository<EmployeeInZone, Guid> _employeeInZoneRepository;

        public EmployeeInZonesAppServiceTests()
        {
            _employeeInZonesAppService = GetRequiredService<IEmployeeInZonesAppService>();
            _employeeInZoneRepository = GetRequiredService<IRepository<EmployeeInZone, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _employeeInZonesAppService.GetListAsync(new GetEmployeeInZonesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.EmployeeInZone.Id == Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeInZone.Id == Guid.Parse("7c756745-3011-407e-b0c8-bcc8093cb4fc")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeInZonesAppService.GetAsync(Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeInZoneCreateDto
            {
                EffectiveDate = new DateTime(2008, 7, 25),
                EndDate = Guid.Parse("db81d370-72c9-444e-b742-d01b1df63f14"),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 7, 25));
            result.EndDate.ShouldBe(Guid.Parse("db81d370-72c9-444e-b742-d01b1df63f14"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2017, 10, 27),
                EndDate = Guid.Parse("a1444ce0-82c4-4a27-9113-3b92fac901cb"),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeInZonesAppService.UpdateAsync(Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"), input);

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2017, 10, 27));
            result.EndDate.ShouldBe(Guid.Parse("a1444ce0-82c4-4a27-9113-3b92fac901cb"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeInZonesAppService.DeleteAsync(Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"));

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"));

            result.ShouldBeNull();
        }
    }
}