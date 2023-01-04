using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;
        private readonly IRepository<CompanyIdentityUserAssignment, Guid> _companyIdentityUserAssignmentRepository;

        public CompanyIdentityUserAssignmentsAppServiceTests()
        {
            _companyIdentityUserAssignmentsAppService = GetRequiredService<ICompanyIdentityUserAssignmentsAppService>();
            _companyIdentityUserAssignmentRepository = GetRequiredService<IRepository<CompanyIdentityUserAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companyIdentityUserAssignmentsAppService.GetListAsync(new GetCompanyIdentityUserAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CompanyIdentityUserAssignment.Id == Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4")).ShouldBe(true);
            result.Items.Any(x => x.CompanyIdentityUserAssignment.Id == Guid.Parse("855a993b-4001-46c7-b2b7-895923744d50")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companyIdentityUserAssignmentsAppService.GetAsync(Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyIdentityUserAssignmentCreateDto
            {
                IdentityUserId = Guid.Parse("33c4be24-1fbf-4878-8fbb-574675f02b8d"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyIdentityUserAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _companyIdentityUserAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IdentityUserId.ShouldBe(Guid.Parse("33c4be24-1fbf-4878-8fbb-574675f02b8d"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyIdentityUserAssignmentUpdateDto()
            {
                IdentityUserId = Guid.Parse("b7eeaf6a-eff1-4f34-90c2-aa07da987132"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyIdentityUserAssignmentsAppService.UpdateAsync(Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"), input);

            // Assert
            var result = await _companyIdentityUserAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IdentityUserId.ShouldBe(Guid.Parse("b7eeaf6a-eff1-4f34-90c2-aa07da987132"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyIdentityUserAssignmentsAppService.DeleteAsync(Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"));

            // Assert
            var result = await _companyIdentityUserAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("27efa87f-4e16-4d17-819b-cc2acbe55cc4"));

            result.ShouldBeNull();
        }
    }
}