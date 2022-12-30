using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;

        public SalesOrgEmpAssignmentRepositoryTests()
        {
            _salesOrgEmpAssignmentRepository = GetRequiredService<ISalesOrgEmpAssignmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgEmpAssignmentRepository.GetListAsync(
                    isBase: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgEmpAssignmentRepository.GetCountAsync(
                    isBase: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}