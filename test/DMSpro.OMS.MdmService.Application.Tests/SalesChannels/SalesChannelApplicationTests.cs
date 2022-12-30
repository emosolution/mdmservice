using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesChannelsAppService _salesChannelsAppService;
        private readonly IRepository<SalesChannel, Guid> _salesChannelRepository;

        public SalesChannelsAppServiceTests()
        {
            _salesChannelsAppService = GetRequiredService<ISalesChannelsAppService>();
            _salesChannelRepository = GetRequiredService<IRepository<SalesChannel, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesChannelsAppService.GetListAsync(new GetSalesChannelsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("40f115d4-22d5-4fa7-9b85-797be4a11c3d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesChannelsAppService.GetAsync(Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesChannelCreateDto
            {
                Code = "16220ff165ef4ab1bc73",
                Name = "2c11f646107e4106aeefb4515298f8da080ddee7367f4bb9a299ff03d1aca6d399981ed6173b4fac8fb8770527065dde11a81cddcf744114bad6c98360bd9f7113205b4d77694e50bf664730acd5b465db8927dff9fe49d0bc4ae5447f23f46b17511d97",
                Description = "eedd019d683845d2a9962b3cc1b76d53094",
                Active = true
            };

            // Act
            var serviceResult = await _salesChannelsAppService.CreateAsync(input);

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("16220ff165ef4ab1bc73");
            result.Name.ShouldBe("2c11f646107e4106aeefb4515298f8da080ddee7367f4bb9a299ff03d1aca6d399981ed6173b4fac8fb8770527065dde11a81cddcf744114bad6c98360bd9f7113205b4d77694e50bf664730acd5b465db8927dff9fe49d0bc4ae5447f23f46b17511d97");
            result.Description.ShouldBe("eedd019d683845d2a9962b3cc1b76d53094");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesChannelUpdateDto()
            {
                Code = "fac67d521c064eb1947c",
                Name = "d247e936afb34e6289a1acee1a159593b2961fb7898948e0900efe37b4523868ef256be8b75948cead930e0d08de0acea940fa5c43c045b785add588316b5e276197d59c74b84659b13c02111cba4dc9960def3c09ee48258d6884f3c6fbb7753e28d775",
                Description = "30bc7e69a7e54004b7aed104984d83dbecebb554ae1",
                Active = true
            };

            // Act
            var serviceResult = await _salesChannelsAppService.UpdateAsync(Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"), input);

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("fac67d521c064eb1947c");
            result.Name.ShouldBe("d247e936afb34e6289a1acee1a159593b2961fb7898948e0900efe37b4523868ef256be8b75948cead930e0d08de0acea940fa5c43c045b785add588316b5e276197d59c74b84659b13c02111cba4dc9960def3c09ee48258d6884f3c6fbb7753e28d775");
            result.Description.ShouldBe("30bc7e69a7e54004b7aed104984d83dbecebb554ae1");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesChannelsAppService.DeleteAsync(Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"));

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"));

            result.ShouldBeNull();
        }
    }
}