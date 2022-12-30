using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public class CustomerAssignmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerAssignmentsAppService _customerAssignmentsAppService;
        private readonly IRepository<CustomerAssignment, Guid> _customerAssignmentRepository;

        public CustomerAssignmentsAppServiceTests()
        {
            _customerAssignmentsAppService = GetRequiredService<ICustomerAssignmentsAppService>();
            _customerAssignmentRepository = GetRequiredService<IRepository<CustomerAssignment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerAssignmentsAppService.GetListAsync(new GetCustomerAssignmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerAssignment.Id == Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAssignment.Id == Guid.Parse("e570c911-ac6d-48ba-9695-6e9d2683c3b8")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAssignmentsAppService.GetAsync(Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAssignmentCreateDto
            {
                EffectiveDate = new DateTime(2017, 8, 20),
                EndDate = new DateTime(2005, 2, 22),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerAssignmentsAppService.CreateAsync(input);

            // Assert
            var result = await _customerAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2017, 8, 20));
            result.EndDate.ShouldBe(new DateTime(2005, 2, 22));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAssignmentUpdateDto()
            {
                EffectiveDate = new DateTime(2016, 8, 16),
                EndDate = new DateTime(2020, 8, 16),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerAssignmentsAppService.UpdateAsync(Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"), input);

            // Assert
            var result = await _customerAssignmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2016, 8, 16));
            result.EndDate.ShouldBe(new DateTime(2020, 8, 16));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAssignmentsAppService.DeleteAsync(Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"));

            // Assert
            var result = await _customerAssignmentRepository.FindAsync(c => c.Id == Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"));

            result.ShouldBeNull();
        }
    }
}