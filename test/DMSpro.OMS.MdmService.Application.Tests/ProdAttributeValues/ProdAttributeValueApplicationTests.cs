using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValuesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IProdAttributeValuesAppService _prodAttributeValuesAppService;
        private readonly IRepository<ProdAttributeValue, Guid> _prodAttributeValueRepository;

        public ProdAttributeValuesAppServiceTests()
        {
            _prodAttributeValuesAppService = GetRequiredService<IProdAttributeValuesAppService>();
            _prodAttributeValueRepository = GetRequiredService<IRepository<ProdAttributeValue, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _prodAttributeValuesAppService.GetListAsync(new GetProdAttributeValuesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ProdAttributeValue.Id == Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c")).ShouldBe(true);
            result.Items.Any(x => x.ProdAttributeValue.Id == Guid.Parse("7f69737f-d2f8-4bc1-bff0-265e3be7e88e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _prodAttributeValuesAppService.GetAsync(Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProdAttributeValueCreateDto
            {
                AttrValName = "9f0d5ddfc1d14393ad9ecca81cfee28afd1ece1a69cd47849c385ea587be3cee8d320cb145584adc95323298b2a58b1e416b",
                ProdAttributeId = Guid.Parse("6048be4f-07df-4015-a5ee-f886ec2077a8"),

            };

            // Act
            var serviceResult = await _prodAttributeValuesAppService.CreateAsync(input);

            // Assert
            var result = await _prodAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("9f0d5ddfc1d14393ad9ecca81cfee28afd1ece1a69cd47849c385ea587be3cee8d320cb145584adc95323298b2a58b1e416b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProdAttributeValueUpdateDto()
            {
                AttrValName = "f02ffff57c2c432a8aebb80a005a3d35b2bea120eeeb455cbada682d84a27f221421cfd4be1749dda901056cc39bff16e04e",
                ProdAttributeId = Guid.Parse("6048be4f-07df-4015-a5ee-f886ec2077a8"),

            };

            // Act
            var serviceResult = await _prodAttributeValuesAppService.UpdateAsync(Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"), input);

            // Assert
            var result = await _prodAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("f02ffff57c2c432a8aebb80a005a3d35b2bea120eeeb455cbada682d84a27f221421cfd4be1749dda901056cc39bff16e04e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _prodAttributeValuesAppService.DeleteAsync(Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"));

            // Assert
            var result = await _prodAttributeValueRepository.FindAsync(c => c.Id == Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"));

            result.ShouldBeNull();
        }
    }
}