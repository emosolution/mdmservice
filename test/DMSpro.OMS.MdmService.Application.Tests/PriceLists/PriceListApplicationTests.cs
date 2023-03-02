using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPriceListsAppService _priceListsAppService;
        private readonly IRepository<PriceList, Guid> _priceListRepository;

        public PriceListsAppServiceTests()
        {
            _priceListsAppService = GetRequiredService<IPriceListsAppService>();
            _priceListRepository = GetRequiredService<IRepository<PriceList, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _priceListsAppService.GetListAsync(new GetPriceListsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PriceList.Id == Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41")).ShouldBe(true);
            result.Items.Any(x => x.PriceList.Id == Guid.Parse("427c26f4-3870-49b3-be11-da4cff578df6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListsAppService.GetAsync(Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListCreateDto
            {
                Code = "bb9db4525af74216b51b",
                Name = "e1e97e8cfef6454988028029f6ff5eb864e4fa258f454a0b88d5e48c9ddbb75247bd29c04469429294507deb8efbba7aa24613c69b7449318fee38ae4ab69a692c5e4413dbb946b38aa0d261df78c85c590e31eaad8748ceaaef0b6245ffbcf8ab7439aed4ae4c40ba1e650b349b704bd04b2b3af3d848acb473ff3ffae84e1",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 96011980,
                ArithmeticFactorType = default,
                IsFirstPriceList = true
            };

            // Act
            var serviceResult = await _priceListsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("bb9db4525af74216b51b");
            result.Name.ShouldBe("e1e97e8cfef6454988028029f6ff5eb864e4fa258f454a0b88d5e48c9ddbb75247bd29c04469429294507deb8efbba7aa24613c69b7449318fee38ae4ab69a692c5e4413dbb946b38aa0d261df78c85c590e31eaad8748ceaaef0b6245ffbcf8ab7439aed4ae4c40ba1e650b349b704bd04b2b3af3d848acb473ff3ffae84e1");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(96011980);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsFirstPriceList.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListUpdateDto()
            {
                Code = "ffca1cb0f43c4c9dbbcd",
                Name = "6fc5163f08584b0d80f16358f36bef5ce91aa7e8aa7c4e9c90d01202da6fb9e90ef427e858ce43e0a736104824e28db05547087feb6448d7be87fe53255e08d493a6be5b12e34fe9a0b1a838ffa243a2c635aea5f22f4403b1f1446b47394bb8bc9c1b48bef54e45b8997a4643995f7450b8dd1280714a3688c362eddd9fbb1",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 1980827665,
                ArithmeticFactorType = default,
                IsFirstPriceList = true
            };

            // Act
            var serviceResult = await _priceListsAppService.UpdateAsync(Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"), input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ffca1cb0f43c4c9dbbcd");
            result.Name.ShouldBe("6fc5163f08584b0d80f16358f36bef5ce91aa7e8aa7c4e9c90d01202da6fb9e90ef427e858ce43e0a736104824e28db05547087feb6448d7be87fe53255e08d493a6be5b12e34fe9a0b1a838ffa243a2c635aea5f22f4403b1f1446b47394bb8bc9c1b48bef54e45b8997a4643995f7450b8dd1280714a3688c362eddd9fbb1");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(1980827665);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsFirstPriceList.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListsAppService.DeleteAsync(Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"));

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"));

            result.ShouldBeNull();
        }
    }
}