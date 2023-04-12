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
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupsAppService.GetListAsync(new GetCustomerGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("95fce899-bd21-4400-9f35-12c36d113f82")).ShouldBe(true);
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
                Status = default,
                Description = "71cdb4246c16421aa976782943125c172a1cdd7e4c0749efa15d19e77e221d1f123e45cf4d90427bbe14da59a28bcc163e0f1a3650144146be12ed666cf68c770ca8d6c222b148c29128458259d25838530e085509aa49b391090073ccd040af6e88348c2a044d75951768f678d0cf0ec8fc4271f44d4e54a1593ac047ca1f3"
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
            result.Description.ShouldBe("71cdb4246c16421aa976782943125c172a1cdd7e4c0749efa15d19e77e221d1f123e45cf4d90427bbe14da59a28bcc163e0f1a3650144146be12ed666cf68c770ca8d6c222b148c29128458259d25838530e085509aa49b391090073ccd040af6e88348c2a044d75951768f678d0cf0ec8fc4271f44d4e54a1593ac047ca1f3");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupUpdateDto()
            {
                Code = "85e5894ad2544eeabda3",
                Name = "c12bf694bc344d588e05d3bf090d22c12bb5a9e2d2af4670a184134e18113323f996bfe3a2df4c3097896dfaa2743c090e1aac789c0e407ab54159fbb8db88a125e8c6fdd954422ba611267dd84598e2587567c610844ada836b5a05e23fde9c29090289fb5f46af8ecb021bc8e8c6c52497b15e5bb8467cabc14b5f12799ef",
                Selectable = true,
                GroupBy = default,
                Status = default,
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
            result.Status.ShouldBe(default);
            result.Description.ShouldBe("e5796352849e4edab745cdaf93740a2e32b457fd2105489fa8422020af023917e7ba2c7829d5433680ed17fa0b09b94e5b26d623c1154a96bd0f6d795c358d2b39a35cb1bc0b4471bf6c133f63a45ce1c2cccef2e05247c1b677e6ff1e6fec5be91972e4a9c5462cb6fe835b7e8f5c07c4518bbec1724cbc840772e507870ec");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupsAppService.DeleteAsync(Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"));

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"));

            result.ShouldBeNull();
        }
    }
}