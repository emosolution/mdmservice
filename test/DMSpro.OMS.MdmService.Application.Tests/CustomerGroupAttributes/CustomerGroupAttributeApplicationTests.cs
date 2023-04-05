using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupAttributesAppService _customerGroupAttributesAppService;
        private readonly IRepository<CustomerGroupAttribute, Guid> _customerGroupAttributeRepository;

        public CustomerGroupAttributesAppServiceTests()
        {
            _customerGroupAttributesAppService = GetRequiredService<ICustomerGroupAttributesAppService>();
            _customerGroupAttributeRepository = GetRequiredService<IRepository<CustomerGroupAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupAttributesAppService.GetListAsync(new GetCustomerGroupAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupAttribute.Id == Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupAttribute.Id == Guid.Parse("635debef-ac63-477b-8826-cb029d906217")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupAttributesAppService.GetAsync(Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupAttributeCreateDto
            {
                Description = "2b32879c2ea041f99ec05dbf55644cb1d0cc664bc652413b84e2665278762c3f9f12117c5cfc4439b36df8a5ac5a8b865d25046ecf4441a982cc5cd2060aad24de929446c3084d7fa2b84c9d5524cc41dee60c33b2b6428cb41ca5dab8ee33bf1824900da7184abd9de40fa2de95c9dca3e369bc7fab4997ab8d5aa1e810058",
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),

            };

            // Act
            var serviceResult = await _customerGroupAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("2b32879c2ea041f99ec05dbf55644cb1d0cc664bc652413b84e2665278762c3f9f12117c5cfc4439b36df8a5ac5a8b865d25046ecf4441a982cc5cd2060aad24de929446c3084d7fa2b84c9d5524cc41dee60c33b2b6428cb41ca5dab8ee33bf1824900da7184abd9de40fa2de95c9dca3e369bc7fab4997ab8d5aa1e810058");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupAttributeUpdateDto()
            {
                Description = "c26f0ac3dd0c4fb8b84df032b5c8d22431d38b470bb945e790bf69db409517e930040b03c5a44a72a4f22080d9b4d6f24c9bff4227b444d5848d633acce57ceb405220d7c455434981ef30622d4081138de7b2e2819d401dacb36e0de580989886d0912a11174642ac9f802d4db895323a00afd22e1644dfb78e31d8b6ef710",
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),

            };

            // Act
            var serviceResult = await _customerGroupAttributesAppService.UpdateAsync(Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"), input);

            // Assert
            var result = await _customerGroupAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("c26f0ac3dd0c4fb8b84df032b5c8d22431d38b470bb945e790bf69db409517e930040b03c5a44a72a4f22080d9b4d6f24c9bff4227b444d5848d633acce57ceb405220d7c455434981ef30622d4081138de7b2e2819d401dacb36e0de580989886d0912a11174642ac9f802d4db895323a00afd22e1644dfb78e31d8b6ef710");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupAttributesAppService.DeleteAsync(Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"));

            // Assert
            var result = await _customerGroupAttributeRepository.FindAsync(c => c.Id == Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"));

            result.ShouldBeNull();
        }
    }
}