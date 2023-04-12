using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupsAppService _customerGroupsAppService;
        private readonly IRepository<CustomerGroup, Guid> _customerGroupRepository;

        public CustomerGroupsAppServiceTests()
        {
            _customerGroupsAppService = GetRequiredService<ICustomerGroupsAppService>();
            _customerGroupRepository = GetRequiredService<IRepository<CustomerGroup, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupsAppService.GetAsync(Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupCreateDto
            {
                Code = "99ccc1613a30435ebca1",
                Name = "01934a399f5f4728b022e187ecef253cbac9ed89436049e38a7649692be644ccff5af6e89bca45f0accc3ea57118837470e50d6f79d84e99a27d9824e4db04662276cfe18f91450598a3bffcf39bfc238c95cef654aa4339a5c4c64b7308d0b4c69679c875194e5aad9c1fb7b53e602cf916f78828824c5cb1c8504f4747dfc",
                Selectable = true,
                GroupBy = default,
                Description = null
            };

            // Act
            var serviceResult = await _customerGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("99ccc1613a30435ebca1");
            result.Name.ShouldBe("01934a399f5f4728b022e187ecef253cbac9ed89436049e38a7649692be644ccff5af6e89bca45f0accc3ea57118837470e50d6f79d84e99a27d9824e4db04662276cfe18f91450598a3bffcf39bfc238c95cef654aa4339a5c4c64b7308d0b4c69679c875194e5aad9c1fb7b53e602cf916f78828824c5cb1c8504f4747dfc");
            result.Selectable.ShouldBe(true);
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(default);
            result.Description.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupUpdateDto()
            {
                Name = "c12bf694bc344d588e05d3bf090d22c12bb5a9e2d2af4670a184134e18113323f996bfe3a2df4c3097896dfaa2743c090e1aac789c0e407ab54159fbb8db88a125e8c6fdd954422ba611267dd84598e2587567c610844ada836b5a05e23fde9c29090289fb5f46af8ecb021bc8e8c6c52497b15e5bb8467cabc14b5f12799ef",
                Selectable = true,
                GroupBy = default,
                Description = "e5796352849e4edab745cdaf93740a2e32b457fd2105489fa8422020af023917e7ba2c7829d5433680ed17fa0b09b94e5b26d623c1154a96bd0f6d795c358d2b39a35cb1bc0b4471bf6c133f63a45ce1c2cccef2e05247c1b677e6ff1e6fec5be91972e4a9c5462cb6fe835b7e8f5c07c4518bbec1724cbc840772e507870ec"
            };

            // Act
            var serviceResult = await _customerGroupsAppService.UpdateAsync(Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"), input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("85e5894ad2544eeabda3");
            result.Name.ShouldBe("c12bf694bc344d588e05d3bf090d22c12bb5a9e2d2af4670a184134e18113323f996bfe3a2df4c3097896dfaa2743c090e1aac789c0e407ab54159fbb8db88a125e8c6fdd954422ba611267dd84598e2587567c610844ada836b5a05e23fde9c29090289fb5f46af8ecb021bc8e8c6c52497b15e5bb8467cabc14b5f12799ef");
            result.Selectable.ShouldBe(true);
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(Status.OPEN);
            result.Description.ShouldBe(null);
        }
    }
}