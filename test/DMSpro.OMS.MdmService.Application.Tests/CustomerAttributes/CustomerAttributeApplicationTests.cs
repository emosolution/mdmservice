using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerAttributesAppService _customerAttributesAppService;
        private readonly IRepository<CustomerAttribute, Guid> _customerAttributeRepository;

        public CustomerAttributesAppServiceTests()
        {
            _customerAttributesAppService = GetRequiredService<ICustomerAttributesAppService>();
            _customerAttributeRepository = GetRequiredService<IRepository<CustomerAttribute, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttributesAppService.GetAsync(Guid.Parse("fe36733c-32c1-4fb0-a391-3764f01cde30"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fe36733c-32c1-4fb0-a391-3764f01cde30"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAttributeCreateDto
            {
                AttrName = "f5fc5383ecab47ec89b881f5669b762ae1614d36b0d34d6b84eafc8ce908213eb77c69d8c0c44452ba7a2fe15c39b0a3a8c6",
            };

            // Act
            var serviceResult = await _customerAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.AttrName == input.AttrName);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(0);
            result.AttrName.ShouldBe("f5fc5383ecab47ec89b881f5669b762ae1614d36b0d34d6b84eafc8ce908213eb77c69d8c0c44452ba7a2fe15c39b0a3a8c6");
            result.Active.ShouldBe(true);
            result.Code.ShouldBe("0");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAttributeUpdateDto()
            {
                AttrName = "ebad5caa53e74645b85be2cb8e76e5962eea1d95a517404fa35dc023f3f0e73a2311141e756c43d8bfef8513dda47a86bba5",
            };

            // Act
            var serviceResult = await _customerAttributesAppService.UpdateAsync(Guid.Parse("fe36733c-32c1-4fb0-a391-3764f01cde30"), input);

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.AttrName == input.AttrName);

            result.ShouldNotBe(null);
            result.AttrName.ShouldBe("ebad5caa53e74645b85be2cb8e76e5962eea1d95a517404fa35dc023f3f0e73a2311141e756c43d8bfef8513dda47a86bba5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAttributesAppService.DeleteAsync();

            // Assert
            var result = await _customerAttributeRepository.FindAsync(c => c.Id == Guid.Parse("fe36733c-32c1-4fb0-a391-3764f01cde30"));

            result.ShouldBeNull();
        }
    }
}