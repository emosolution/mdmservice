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
            result.Items.Any(x => x.Id == Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("f28f4bcc-161b-4637-82f9-478e41abb83c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupsAppService.GetAsync(Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupCreateDto
            {
                Code = "ff1e2686cc454eb1a036",
                Name = "eeb07d56c4df497da6adcd75dfd9ce7fb6e9609ee8e3477cbbddb6f054554e45fbfca807a71c45f9b484a490d4e11be2c87309d78521438db487fb015fa47762855eb1f984ed416da8a79ad203bb53b3a6ecafb0d5b74e02b91ed48f9b6dd0d1b053d9afdbdc4eed8e462d58b01985152bde86a42ef8440b815ec22c792535c",
                Active = true,
                EffectiveDate = new DateTime(2017, 8, 23),
                GroupBy = default,
                Status = default
            };

            // Act
            var serviceResult = await _customerGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ff1e2686cc454eb1a036");
            result.Name.ShouldBe("eeb07d56c4df497da6adcd75dfd9ce7fb6e9609ee8e3477cbbddb6f054554e45fbfca807a71c45f9b484a490d4e11be2c87309d78521438db487fb015fa47762855eb1f984ed416da8a79ad203bb53b3a6ecafb0d5b74e02b91ed48f9b6dd0d1b053d9afdbdc4eed8e462d58b01985152bde86a42ef8440b815ec22c792535c");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2017, 8, 23));
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupUpdateDto()
            {
                Code = "ad719b7ac4964d90ae4d",
                Name = "909d9c8d51cc41bf9d344eb2dd4020bea03e3e7c84e94d2eb0a603bde0667288a2f76dcd2e7c487bbf4a1449cef049bb8ba39506f0094139a091d442dae02e847958ce701dee403ba5a09aa761381d39a032864fcf374a518499985c4dc5f5a9a931dd400a294a4c860459e3c633afbc57061bec8a4c4748be21ecb45bcdad5",
                Active = true,
                EffectiveDate = new DateTime(2008, 3, 6),
                GroupBy = default,
                Status = default
            };

            // Act
            var serviceResult = await _customerGroupsAppService.UpdateAsync(Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"), input);

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ad719b7ac4964d90ae4d");
            result.Name.ShouldBe("909d9c8d51cc41bf9d344eb2dd4020bea03e3e7c84e94d2eb0a603bde0667288a2f76dcd2e7c487bbf4a1449cef049bb8ba39506f0094139a091d442dae02e847958ce701dee403ba5a09aa761381d39a032864fcf374a518499985c4dc5f5a9a931dd400a294a4c860459e3c633afbc57061bec8a4c4748be21ecb45bcdad5");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 3, 6));
            result.GroupBy.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupsAppService.DeleteAsync(Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"));

            // Assert
            var result = await _customerGroupRepository.FindAsync(c => c.Id == Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"));

            result.ShouldBeNull();
        }
    }
}