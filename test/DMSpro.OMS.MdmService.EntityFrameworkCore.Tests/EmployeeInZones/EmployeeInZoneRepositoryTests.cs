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
                    endDate: Guid.Parse("6c47627f-7496-401a-90db-59b89dd7019e")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("be95496e-4382-4f20-ab36-cc18be96ebe8"));
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
                    endDate: Guid.Parse("be0d0193-1f6e-4489-adbd-76dcb8d14f93")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}