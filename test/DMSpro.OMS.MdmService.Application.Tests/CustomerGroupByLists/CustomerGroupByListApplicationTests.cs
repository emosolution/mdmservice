using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupByListsAppService _customerGroupByListsAppService;
        private readonly IRepository<CustomerGroupByList, Guid> _customerGroupByListRepository;

        public CustomerGroupByListsAppServiceTests()
        {
            _customerGroupByListsAppService = GetRequiredService<ICustomerGroupByListsAppService>();
            _customerGroupByListRepository = GetRequiredService<IRepository<CustomerGroupByList, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupByListsAppService.GetListAsync(new GetCustomerGroupByListsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupByList.Id == Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupByList.Id == Guid.Parse("a11e04cd-8619-4bea-8f5f-27608f47e0da")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupByListsAppService.GetAsync(Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupByListCreateDto
            {
                Active = true,
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerGroupByListsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupByListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupByListUpdateDto()
            {
                Active = true,
                CustomerGroupId = Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerGroupByListsAppService.UpdateAsync(Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"), input);

            // Assert
            var result = await _customerGroupByListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupByListsAppService.DeleteAsync(Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"));

            // Assert
            var result = await _customerGroupByListRepository.FindAsync(c => c.Id == Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"));

            result.ShouldBeNull();
        }
    }
}