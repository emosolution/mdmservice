using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValuesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerAttributeValuesAppService _customerAttributeValuesAppService;
        private readonly IRepository<CustomerAttributeValue, Guid> _customerAttributeValueRepository;

        public CustomerAttributeValuesAppServiceTests()
        {
            _customerAttributeValuesAppService = GetRequiredService<ICustomerAttributeValuesAppService>();
            _customerAttributeValueRepository = GetRequiredService<IRepository<CustomerAttributeValue, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttributeValuesAppService.GetAsync(Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAttributeValueCreateDto
            {
                Code = "7b54c4004e184f9f8e08",
                AttrValName = "d2926206d93042728f930465039607ae59f2cb6d47fd49879eea7dc30de49375de8a4ce3342542fe88f6ac966f7d39c8c4960bc7081b498aae36269e22ae098b9c17760460fe4c0fa77b41028c3fb5fa37a8a626b38f44cfb213064c1e4f8caf58ff4f2dd6f14e01b5d7a820c149de5d4988c4b2603c4b7f9e0d4cacae754ce",
                CustomerAttributeId = Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),

            };

            // Act
            var serviceResult = await _customerAttributeValuesAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7b54c4004e184f9f8e08");
            result.AttrValName.ShouldBe("d2926206d93042728f930465039607ae59f2cb6d47fd49879eea7dc30de49375de8a4ce3342542fe88f6ac966f7d39c8c4960bc7081b498aae36269e22ae098b9c17760460fe4c0fa77b41028c3fb5fa37a8a626b38f44cfb213064c1e4f8caf58ff4f2dd6f14e01b5d7a820c149de5d4988c4b2603c4b7f9e0d4cacae754ce");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAttributeValueUpdateDto()
            {
                AttrValName = "410dcaf5343447a4875cf26e50d7d19942dffb402b2e42558a3382428bcde0358b1d254a13b74670a10a55c80d445a2eab30d0afaa90485eb1e7ff384f8d8add5afc22c16142483c989750ec87e8d59ef11aa91ce2814d49a114b69a63540f161c397e9b108840e3a4fe49ae7a0158f4dfaef930c5474e9dbc251c88399d903",

            };

            // Act
            var serviceResult = await _customerAttributeValuesAppService.UpdateAsync(Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"), input);

            // Assert
            var result = await _customerAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("410dcaf5343447a4875cf26e50d7d19942dffb402b2e42558a3382428bcde0358b1d254a13b74670a10a55c80d445a2eab30d0afaa90485eb1e7ff384f8d8add5afc22c16142483c989750ec87e8d59ef11aa91ce2814d49a114b69a63540f161c397e9b108840e3a4fe49ae7a0158f4dfaef930c5474e9dbc251c88399d903");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAttributeValuesAppService.DeleteAsync(Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"));

            // Assert
            var result = await _customerAttributeValueRepository.FindAsync(c => c.Id == Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"));

            result.ShouldBeNull();
        }
    }
}