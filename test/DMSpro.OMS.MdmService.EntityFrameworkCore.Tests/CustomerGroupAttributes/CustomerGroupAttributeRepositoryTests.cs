using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;

        public CustomerGroupAttributeRepositoryTests()
        {
            _customerGroupAttributeRepository = GetRequiredService<ICustomerGroupAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupAttributeRepository.GetListAsync(
                    description: "f236584e67d64e88941ac898345d28ede0dd53708cd141ec98db4a34fed5a1595374c67cf9e24eb9a4d021030382c18dc8c0498748a6477db38eb49df566306c0e20cb6509734293919a67b7ece9fdc3ae38a842b0054f05b1ad918ac44daa5a63504448ed444233bdafe9d44e35b77479df729a887344049923992e49ff40b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerGroupAttributeRepository.GetCountAsync(
                    description: "33c25055df434447bc4b8f2fbced745f424bf0f91df44c77939703faea6368fa50e8fbfb037b47f7af024e6ff16a0a6c413e05773c22484e8119969249f0bb8d71fdadba48244d55975f34077c11fe408e7f3dfd55084fe0bb6e3fec1e0484586980f6b908eb4e56960b2aaedd266adfb4c52fa449ed4f84a3f2d889017ab59"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}