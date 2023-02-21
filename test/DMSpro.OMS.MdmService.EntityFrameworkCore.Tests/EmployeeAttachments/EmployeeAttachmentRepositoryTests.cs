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
                    description: "26e777a795bf400aaf17eba2ee40fe6e71d42ca04db94b408b3f",
                    active: true,
                    fileId: Guid.Parse("8d715ab0-e548-472b-9fba-da58d5114160")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"));
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
                    description: "4efa62fe5eb94042a5ae95bf11aff2f31f43d664c8d547e9b8b8aa8dcd4cc7b9fe3",
                    active: true,
                    fileId: Guid.Parse("68e3715d-52ee-492b-8804-4e38b7cc2f36")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}