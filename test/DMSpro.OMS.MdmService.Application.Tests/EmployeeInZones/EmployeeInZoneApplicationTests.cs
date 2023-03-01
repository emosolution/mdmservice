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
            result.Items.Any(x => x.EmployeeInZone.Id == Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeInZone.Id == Guid.Parse("0c446f99-9591-42d1-ae7a-2016ab9db854")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeInZonesAppService.GetAsync(Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeInZoneCreateDto
            {
                EffectiveDate = new DateTime(2022, 7, 7),
                EndDate = new DateTime(2006, 9, 12),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2022, 7, 7));
            result.EndDate.ShouldBe(new DateTime(2006, 9, 12));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2014, 6, 3),
                EndDate = new DateTime(2019, 4, 23),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeInZonesAppService.UpdateAsync(Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"), input);

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2014, 6, 3));
            result.EndDate.ShouldBe(new DateTime(2019, 4, 23));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeInZonesAppService.DeleteAsync(Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"));

            // Assert
            var result = await _employeeInZoneRepository.FindAsync(c => c.Id == Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"));

            result.ShouldBeNull();
        }
    }
}