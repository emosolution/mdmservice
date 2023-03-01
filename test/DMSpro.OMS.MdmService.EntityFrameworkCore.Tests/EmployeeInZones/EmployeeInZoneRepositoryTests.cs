using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EmployeeInZones;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public class EmployeeInZoneRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeInZoneRepository _employeeInZoneRepository;

        public EmployeeInZoneRepositoryTests()
        {
            _employeeInZoneRepository = GetRequiredService<IEmployeeInZoneRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeInZoneRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5ad99832-9ebd-4aa5-87bd-230e7e14caf4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeInZoneRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}