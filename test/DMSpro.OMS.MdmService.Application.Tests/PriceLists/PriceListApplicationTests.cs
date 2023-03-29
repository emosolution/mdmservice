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
            var result = await _priceListsAppService.GetAsync(Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListCreateDto
            {
                Code = "a77eb2f06ac4453694d1",
                Name = "82b66a969bd4466c94517c0d7e9579fb4964085cf40a4c228a653f0f3656d5428d8a03c09ae54c08b26f1696b9190705d1f7cf1e462542dd92180b7bed70cdcf37d8208c783a4e29bddf45c55869a25f6df6891ed8ba44d8a37a08871374c1307b2e9cfb5645454f9b094f17ce13dd873bf25da1a84c49e7aae61611f8039bc",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 1253240624,
                ArithmeticFactorType = default,
                IsDefault = true,
            };

            // Act
            var serviceResult = await _priceListsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("a77eb2f06ac4453694d1");
            result.Name.ShouldBe("82b66a969bd4466c94517c0d7e9579fb4964085cf40a4c228a653f0f3656d5428d8a03c09ae54c08b26f1696b9190705d1f7cf1e462542dd92180b7bed70cdcf37d8208c783a4e29bddf45c55869a25f6df6891ed8ba44d8a37a08871374c1307b2e9cfb5645454f9b094f17ce13dd873bf25da1a84c49e7aae61611f8039bc");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(1253240624);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefault.ShouldBe(true);
            result.IsReleased.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListUpdateDto()
            {
                Code = "7acc9c141ff94a40908a",
                Name = "90b87e5aa84d462e830935bcea14202a495c7328467148f9af09799cf8701fc91f7995206ac041339c36433adb2797579a540bb42be5480d8ef344730d40e4a43cf3838b197f4bbeba64a3c4209f0cf7316070b938e1436eaaec27ee727c3988662a980bb46941a1be35640086b66b90ec3c3d93752841daa35444e555511c6",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 2029648485,
                ArithmeticFactorType = default,
                IsDefault = true,
            };

            // Act
            var serviceResult = await _priceListsAppService.UpdateAsync(Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"), input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7acc9c141ff94a40908a");
            result.Name.ShouldBe("90b87e5aa84d462e830935bcea14202a495c7328467148f9af09799cf8701fc91f7995206ac041339c36433adb2797579a540bb42be5480d8ef344730d40e4a43cf3838b197f4bbeba64a3c4209f0cf7316070b938e1436eaaec27ee727c3988662a980bb46941a1be35640086b66b90ec3c3d93752841daa35444e555511c6");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(2029648485);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefault.ShouldBe(true);
            result.IsReleased.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListsAppService.DeleteAsync(Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"));

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == Guid.Parse("e12fe94e-91f7-474f-a74b-c83fc1fce713"));

            result.ShouldBeNull();
        }
    }
}