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
        public async Task GetAsync()
        {
            // Act
            var result = await _priceListsAppService.GetAsync(Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListCreateDto
            {
                Code = "73360ac1562246458aa6",
                Name = "4b067d192c3547329a2a5111862dff672f053d053e654781b143c4f86157ad64d7e4f460b6144303b87e68f52ee1b72b934b221fec4b4c9083e6713f745a4a1a1bb77d1f16fe41e094fbe7824c2184977eb97f7ce59a467a9554da0c7dbff30744a28ba0c3ed431c819215b8aea6866f06087bab39b04687842262803510c97",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 42319097,
                ArithmeticFactorType = default,
                IsDefaultForCustomer = true,
                IsDefaultForVendor = true
            };

            // Act
            var serviceResult = await _priceListsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("73360ac1562246458aa6");
            result.Name.ShouldBe("4b067d192c3547329a2a5111862dff672f053d053e654781b143c4f86157ad64d7e4f460b6144303b87e68f52ee1b72b934b221fec4b4c9083e6713f745a4a1a1bb77d1f16fe41e094fbe7824c2184977eb97f7ce59a467a9554da0c7dbff30744a28ba0c3ed431c819215b8aea6866f06087bab39b04687842262803510c97");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(42319097);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefaultForCustomer.ShouldBe(true);
            result.IsReleased.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
            result.IsDefaultForVendor.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListUpdateDto()
            {
                Code = "36ec8128d0214722b6f7",
                Name = "aafc125b18564dd19433332c178321200a75e8027cdb446a833f1f50759d9567c1ef1f7cf6284ccfb4e7f5f36138668cf4a5d833ce704767b992639fa8445895f2689d9dea064449bda9dc0599f56c5365b5ce64e58f45cf827ba9fc5913d7448b0e12a74e4f4103a54fa00728450b77bf80e19ea5ca4cb586953a4cbff6fd6",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 1616182308,
                ArithmeticFactorType = default,
                IsDefaultForCustomer = true,
                IsDefaultForVendor = true
            };

            // Act
            var serviceResult = await _priceListsAppService.UpdateAsync(Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"), input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("36ec8128d0214722b6f7");
            result.Name.ShouldBe("aafc125b18564dd19433332c178321200a75e8027cdb446a833f1f50759d9567c1ef1f7cf6284ccfb4e7f5f36138668cf4a5d833ce704767b992639fa8445895f2689d9dea064449bda9dc0599f56c5365b5ce64e58f45cf827ba9fc5913d7448b0e12a74e4f4103a54fa00728450b77bf80e19ea5ca4cb586953a4cbff6fd6");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(1616182308);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefaultForCustomer.ShouldBe(true);
            result.IsReleased.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
            result.IsDefaultForVendor.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListsAppService.DeleteAsync(Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"));

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083"));

            result.ShouldBeNull();
        }
    }
}