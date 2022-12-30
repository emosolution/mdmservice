using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;

        public EmployeeAttachmentRepositoryTests()
        {
            _employeeAttachmentRepository = GetRequiredService<IEmployeeAttachmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeAttachmentRepository.GetListAsync(
                    url: "206cf36bd3e04993ba656893752a6ef089d4872b2a40402dad9eca74",
                    description: "6f9435",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _employeeAttachmentRepository.GetCountAsync(
                    url: "1f90d0ccdcbe4569af2338336044715984974",
                    description: "523351348e834e219b17",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}