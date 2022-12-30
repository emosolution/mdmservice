using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesOrgEmpAssignmentsAppService _salesOrgEmpAssignmentsAppService;
        private readonly IRepository<SalesOrgEmpAssignment, Guid> _salesOrgEmpAssignmentRepository;

        public SalesOrgEmpAssignmentsAppServiceTests()
        {
            _salesOrgEmpAssignmentsAppService = GetRequiredService<ISalesOrgEmpAssignmentsAppService>();
            _salesOrgEmpAssignmentRepository = GetRequiredService<IRepository<SalesOrgEmpAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesOrgEmpAssignmentsAppService.GetListAsync(new GetSalesOrgEmpAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.SalesOrgEmpAssignment.Id == Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a")).ShouldBe(true);
            result.Items.Any(x => x.SalesOrgEmpAssignment.Id == Guid.Parse("11051961-14d4-454e-965d-d2502af59e8b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgEmpAssignmentsAppService.GetAsync(Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentCreateDto
            {
                IsBase = true,
                EffectiveDate = new DateTime(2010, 10, 11),
                EndDate = new DateTime(2015, 5, 3),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2010, 10, 11));
            result.EndDate.ShouldBe(new DateTime(2015, 5, 3));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentUpdateDto()
            {
                IsBase = true,
                EffectiveDate = new DateTime(2015, 3, 7),
                EndDate = new DateTime(2015, 5, 14),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.UpdateAsync(Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"), input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2015, 3, 7));
            result.EndDate.ShouldBe(new DateTime(2015, 5, 14));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgEmpAssignmentsAppService.DeleteAsync(Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"));

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("96d9bae3-9a5b-483b-a9bc-0e164419d17a"));

            result.ShouldBeNull();
        }
    }
}