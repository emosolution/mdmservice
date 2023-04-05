using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValuesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemAttributeValuesAppService _itemAttributeValuesAppService;
        private readonly IRepository<ItemAttributeValue, Guid> _itemAttributeValueRepository;

        public ItemAttributeValuesAppServiceTests()
        {
            _itemAttributeValuesAppService = GetRequiredService<IItemAttributeValuesAppService>();
            _itemAttributeValueRepository = GetRequiredService<IRepository<ItemAttributeValue, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttributeValuesAppService.GetAsync(Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"));
        }

        [Fact]
        public async Task CreateRootAsync()
        {
            // Arrange
            var input = new ItemAttributeValueCreateRootDto
            {
                AttrValName = "5a3d3efa016e4c0594b807b9745cdb113927af82dfbe452f97c763eaba8a4ccae6faef8ec7a94995b831ce3929330bdfd6a975d6a4bc4f278fbe52e176a1478feb3038352b1a441cb751f9b448df14c9764ffb70c2034bce83c7a83c7599095d04fa9e6379774e448bd232e9f0e2bbe9cc0391a91e56491a94f599ac8802566",
                Code = "5a3d3efa016e4c0594b807b9745cdb113927af82dfbe452f97c763eaba8a4ccae6faef8ec7a94995b831ce3929330bdfd6a975d6a4bc4f278fbe52e176a1478feb3038352b1a441cb751f9b448df14c9764ffb70c2034bce83c7a83c7599095d04fa9e6379774e448bd232e9f0e2bbe9cc0391a91e56491a94f599ac8802566",
            };

            // Act
            var serviceResult = await _itemAttributeValuesAppService.CreateRootAsync(input);

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("5a3d3efa016e4c0594b807b9745cdb113927af82dfbe452f97c763eaba8a4ccae6faef8ec7a94995b831ce3929330bdfd6a975d6a4bc4f278fbe52e176a1478feb3038352b1a441cb751f9b448df14c9764ffb70c2034bce83c7a83c7599095d04fa9e6379774e448bd232e9f0e2bbe9cc0391a91e56491a94f599ac8802566");
            result.Code.ShouldBe("5a3d3efa016e4c0594b807b9745cdb113927af82dfbe452f97c763eaba8a4ccae6faef8ec7a94995b831ce3929330bdfd6a975d6a4bc4f278fbe52e176a1478feb3038352b1a441cb751f9b448df14c9764ffb70c2034bce83c7a83c7599095d04fa9e6379774e448bd232e9f0e2bbe9cc0391a91e56491a94f599ac8802566");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttributeValueUpdateDto()
            {
                AttrValName = "b2722eb53d1742259a18a35c20f81a4c3345b21d6fea45f38b13bbb2694ef308ab4067182b904f6fbedd61b8559108080fa7c6336c4b4dcbbecbd55cef894646614a1748cc2d46769e97db511b6c4eaf86f6a3c5f4c14b04a6adb5ebe055c6bab370b9e0366f45baad4a61199f8e79a16f3584be5aae482a866e6d5e09c3ba5",
            };

            // Act
            var serviceResult = await _itemAttributeValuesAppService.UpdateAsync(Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"), input);

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("b2722eb53d1742259a18a35c20f81a4c3345b21d6fea45f38b13bbb2694ef308ab4067182b904f6fbedd61b8559108080fa7c6336c4b4dcbbecbd55cef894646614a1748cc2d46769e97db511b6c4eaf86f6a3c5f4c14b04a6adb5ebe055c6bab370b9e0366f45baad4a61199f8e79a16f3584be5aae482a866e6d5e09c3ba5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttributeValuesAppService.DeleteAsync(Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"));

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == Guid.Parse("37e1733b-b6e1-43d9-879e-e0fcaaff1da4"));

            result.ShouldBeNull();
        }
    }
}