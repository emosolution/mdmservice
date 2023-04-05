using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupListsAppService _customerGroupListsAppService;
        private readonly IRepository<CustomerGroupList, Guid> _customerGroupListRepository;

        public CustomerGroupListsAppServiceTests()
        {
            _customerGroupListsAppService = GetRequiredService<ICustomerGroupListsAppService>();
            _customerGroupListRepository = GetRequiredService<IRepository<CustomerGroupList, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupListsAppService.GetListAsync(new GetCustomerGroupListsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupList.Id == Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupList.Id == Guid.Parse("bd122a43-609a-4d91-ba43-b74af4e22391")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupListsAppService.GetAsync(Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupListCreateDto
            {
                Description = "6283ceadaa65478dbedc",
                Active = true,
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _customerGroupListsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("6283ceadaa65478dbedc");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupListUpdateDto()
            {
                Description = "21cd6891f3c447cf9258",
                Active = true,
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            };

            // Act
            var serviceResult = await _customerGroupListsAppService.UpdateAsync(Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"), input);

            // Assert
            var result = await _customerGroupListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("21cd6891f3c447cf9258");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupListsAppService.DeleteAsync(Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"));

            // Assert
            var result = await _customerGroupListRepository.FindAsync(c => c.Id == Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"));

            result.ShouldBeNull();
        }
    }
}