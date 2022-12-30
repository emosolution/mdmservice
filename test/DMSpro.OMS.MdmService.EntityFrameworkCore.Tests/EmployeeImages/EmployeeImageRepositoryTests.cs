using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeImageRepository _employeeImageRepository;

        public EmployeeImageRepositoryTests()
        {
            _employeeImageRepository = GetRequiredService<IEmployeeImageRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeImageRepository.GetListAsync(
                    description: "05ea0976e2c043b396c8baa6a524eae6052d04b6",
                    url: "d92ca216f96a4123b0448ea2c4f686acc2d450e09d71453081a34e",
                    active: true,
                    isAvatar: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeImageRepository.GetCountAsync(
                    description: "4b34c82c146e4ae58c8bab953f817b1478cb75ac54b549dd875d86bdeba2a11ebadd81b9b05e4c87a5b9f393810b77a",
                    url: "a16b398896d941a1a51e02832f9cc78f7dcd26d6d2d143bfba572583555ab38bf335a4db42cc45eeb852bf37",
                    active: true,
                    isAvatar: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}