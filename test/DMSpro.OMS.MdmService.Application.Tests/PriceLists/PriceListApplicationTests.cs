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
            var result = await _priceListsAppService.GetAsync(Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceListCreateDto
            {
                Code = "9fcb898b3907412a951e",
                Name = "f484f7a3a3074ca881fda113d007bb2173d3466faf504d3fae5d3389817738cfff1a1c64dc1749909db159a9b35ba21040c98273bb8f4f4aae7cae483b96880d4b267840aac644ff917e6bffbc667300b35d1e5f95994166a7c8fa662949343fe9e94e894abf4bf3b3818f094e60ce3e7dfbbf7dc188485da41820b42321bb8",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 524263729,
                ArithmeticFactorType = default,
                IsDefault = true
            };

            // Act
            var serviceResult = await _priceListsAppService.CreateAsync(input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("9fcb898b3907412a951e");
            result.Name.ShouldBe("f484f7a3a3074ca881fda113d007bb2173d3466faf504d3fae5d3389817738cfff1a1c64dc1749909db159a9b35ba21040c98273bb8f4f4aae7cae483b96880d4b267840aac644ff917e6bffbc667300b35d1e5f95994166a7c8fa662949343fe9e94e894abf4bf3b3818f094e60ce3e7dfbbf7dc188485da41820b42321bb8");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(524263729);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceListUpdateDto()
            {
                Code = "05e9b81e2d1c4a7ea2a6",
                Name = "29b7b46b845a41c2b9952bd916e7186597e8449df5c847e0abc1a99987318a253196c988872a4dbc921976680643be16f13a190156634bac9b1966062dbb43d1ba5e82703750414486a460ba330fc0b58c9dfad6bc7948b59686a59d76beb13d96888a4337a547e3823aced120c38959f8a1e6d3cb304246aa1ebf19e3883e6",
                Active = true,
                ArithmeticOperation = default,
                ArithmeticFactor = 424901024,
                ArithmeticFactorType = default,
                IsDefault = true
            };

            // Act
            var serviceResult = await _priceListsAppService.UpdateAsync(Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"), input);

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("05e9b81e2d1c4a7ea2a6");
            result.Name.ShouldBe("29b7b46b845a41c2b9952bd916e7186597e8449df5c847e0abc1a99987318a253196c988872a4dbc921976680643be16f13a190156634bac9b1966062dbb43d1ba5e82703750414486a460ba330fc0b58c9dfad6bc7948b59686a59d76beb13d96888a4337a547e3823aced120c38959f8a1e6d3cb304246aa1ebf19e3883e6");
            result.Active.ShouldBe(true);
            result.ArithmeticOperation.ShouldBe(default);
            result.ArithmeticFactor.ShouldBe(424901024);
            result.ArithmeticFactorType.ShouldBe(default);
            result.IsBase.ShouldBe(false);
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceListsAppService.DeleteAsync(Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"));

            // Assert
            var result = await _priceListRepository.FindAsync(c => c.Id == Guid.Parse("3158ddf9-eb6a-4e20-b24f-7c0161437270"));

            result.ShouldBeNull();
        }
    }
}