using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public CompanyIdentityUserAssignmentRepositoryTests()
        {
            _companyIdentityUserAssignmentRepository = GetRequiredService<ICompanyIdentityUserAssignmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyIdentityUserAssignmentRepository.GetListAsync(
                    identityUserId: Guid.Parse("c3ec831c-f0a5-4963-b161-3d1e0d33d458")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyIdentityUserAssignmentRepository.GetCountAsync(
                    identityUserId: Guid.Parse("270bd74a-2869-4f01-8810-3321b992288e")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}