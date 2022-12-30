using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupByListRepository _customerGroupByListRepository;

        public CustomerGroupByListRepositoryTests()
        {
            _customerGroupByListRepository = GetRequiredService<ICustomerGroupByListRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByListRepository.GetListAsync(
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupByListRepository.GetCountAsync(
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}