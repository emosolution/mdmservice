using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupByAttsAppService _customerGroupByAttsAppService;
        private readonly IRepository<CustomerGroupByAtt, Guid> _customerGroupByAttRepository;

        public CustomerGroupByAttsAppServiceTests()
        {
            _customerGroupByAttsAppService = GetRequiredService<ICustomerGroupByAttsAppService>();
            _customerGroupByAttRepository = GetRequiredService<IRepository<CustomerGroupByAtt, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupByAttsAppService.GetListAsync(new GetCustomerGroupByAttsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupByAtt.Id == Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupByAtt.Id == Guid.Parse("4d7f0e31-431c-4200-aa08-b7b2ed05f27a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupByAttsAppService.GetAsync(Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupByAttCreateDto
            {
                ValueCode = "88b5b67d8dea4b90bc59",
                ValueName = "7d405c1f290a48a4b1decfda7fdee1de03bc18640f8e4eb59918aa67760296c894675e9a9d5d4b9f84609dac5a8474d88a229f2e24da490a90a96d6e4432b73cefea04f240f54403b867b11032146aa20d37d5d340644e96bf2818895efa0b8a240ca23461044d4c85f2267d18476f9f5a865ef8a47147b686f9fccb31801f0",
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                CusAttributeValueId = Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            };

            // Act
            var serviceResult = await _customerGroupByAttsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueCode.ShouldBe("88b5b67d8dea4b90bc59");
            result.ValueName.ShouldBe("7d405c1f290a48a4b1decfda7fdee1de03bc18640f8e4eb59918aa67760296c894675e9a9d5d4b9f84609dac5a8474d88a229f2e24da490a90a96d6e4432b73cefea04f240f54403b867b11032146aa20d37d5d340644e96bf2818895efa0b8a240ca23461044d4c85f2267d18476f9f5a865ef8a47147b686f9fccb31801f0");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupByAttUpdateDto()
            {
                ValueCode = "f7ec87f976b44416b066",
                ValueName = "0b7078d3b3f74aaea67db3c898104a94c34d3162f3794e5190dbfbf1703be1cd90832ebef2d541b989ea144b60301f16f53f9700feaf46ef843b6f10408257b711e5a6bd806440d1bfbe1d4545c56cc6de44a499c1f24908a22ac37c115b4b49969b98654d8c4094bfdcdec8f2846920a67bf951052c4f9899f805be75d48d8",
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                CusAttributeValueId = Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            };

            // Act
            var serviceResult = await _customerGroupByAttsAppService.UpdateAsync(Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"), input);

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ValueCode.ShouldBe("f7ec87f976b44416b066");
            result.ValueName.ShouldBe("0b7078d3b3f74aaea67db3c898104a94c34d3162f3794e5190dbfbf1703be1cd90832ebef2d541b989ea144b60301f16f53f9700feaf46ef843b6f10408257b711e5a6bd806440d1bfbe1d4545c56cc6de44a499c1f24908a22ac37c115b4b49969b98654d8c4094bfdcdec8f2846920a67bf951052c4f9899f805be75d48d8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupByAttsAppService.DeleteAsync(Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"));

            // Assert
            var result = await _customerGroupByAttRepository.FindAsync(c => c.Id == Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"));

            result.ShouldBeNull();
        }
    }
}