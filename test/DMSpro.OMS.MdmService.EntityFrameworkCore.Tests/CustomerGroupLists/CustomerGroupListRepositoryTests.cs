using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupListRepository _customerGroupListRepository;

        public CustomerGroupListRepositoryTests()
        {
            _customerGroupListRepository = GetRequiredService<ICustomerGroupListRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupListRepository.GetListAsync(
                    description: "32de6dc4b5a3461fab05",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupListRepository.GetCountAsync(
                    description: "c38a3c86e33e4bbdafc0",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}