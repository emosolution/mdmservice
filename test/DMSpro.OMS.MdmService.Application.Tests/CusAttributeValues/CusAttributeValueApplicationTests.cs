using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValuesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICusAttributeValuesAppService _cusAttributeValuesAppService;
        private readonly IRepository<CusAttributeValue, Guid> _cusAttributeValueRepository;

        public CusAttributeValuesAppServiceTests()
        {
            _cusAttributeValuesAppService = GetRequiredService<ICusAttributeValuesAppService>();
            _cusAttributeValueRepository = GetRequiredService<IRepository<CusAttributeValue, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cusAttributeValuesAppService.GetListAsync(new GetCusAttributeValuesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CusAttributeValue.Id == Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")).ShouldBe(true);
            result.Items.Any(x => x.CusAttributeValue.Id == Guid.Parse("14db2331-958b-4622-8856-4e1949cdc323")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cusAttributeValuesAppService.GetAsync(Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CusAttributeValueCreateDto
            {
                AttrValName = "cbe444e2511345088ca4b21ee1a2da00a8bacd2dff5a4af5960b7e0246b0bb1710ffcfedd27740fc931d0a28d3c8df517ed4",
                CustomerAttributeId = Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),

            };

            // Act
            var serviceResult = await _cusAttributeValuesAppService.CreateAsync(input);

            // Assert
            var result = await _cusAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("cbe444e2511345088ca4b21ee1a2da00a8bacd2dff5a4af5960b7e0246b0bb1710ffcfedd27740fc931d0a28d3c8df517ed4");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CusAttributeValueUpdateDto()
            {
                AttrValName = "db530099ec0f448abe2e268f8c091b5ed4bf64efd1d9445f8ae3305c16ceaacd0c47b911e63c4ccaa8cceff28417923dbdbf",
                CustomerAttributeId = Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),

            };

            // Act
            var serviceResult = await _cusAttributeValuesAppService.UpdateAsync(Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"), input);

            // Assert
            var result = await _cusAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("db530099ec0f448abe2e268f8c091b5ed4bf64efd1d9445f8ae3305c16ceaacd0c47b911e63c4ccaa8cceff28417923dbdbf");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cusAttributeValuesAppService.DeleteAsync(Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"));

            // Assert
            var result = await _cusAttributeValueRepository.FindAsync(c => c.Id == Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"));

            result.ShouldBeNull();
        }
    }
}