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
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgEmpAssignmentsAppService.GetAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentCreateDto
            {
                IsBase = true,
                EffectiveDate = new DateTime(2018, 9, 15),
                EndDate = new DateTime(2018, 7, 12),
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2018, 9, 15));
            result.EndDate.ShouldBe(new DateTime(2018, 7, 12));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgEmpAssignmentUpdateDto()
            {
                IsBase = true,
                EffectiveDate = new DateTime(2008, 2, 3),
                EndDate = new DateTime(2002, 7, 10),
            };

            // Act
            var serviceResult = await _salesOrgEmpAssignmentsAppService.UpdateAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"), input);

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsBase.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 2, 3));
            result.EndDate.ShouldBe(new DateTime(2002, 7, 10));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgEmpAssignmentsAppService.DeleteAsync(Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            // Assert
            var result = await _salesOrgEmpAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("60067dee-66de-400a-8300-0c7a70249917"));

            result.ShouldBeNull();
        }
    }
}