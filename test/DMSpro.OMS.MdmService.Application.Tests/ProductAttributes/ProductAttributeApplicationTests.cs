using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IProductAttributesAppService _productAttributesAppService;
        private readonly IRepository<ProductAttribute, Guid> _productAttributeRepository;

        public ProductAttributesAppServiceTests()
        {
            _productAttributesAppService = GetRequiredService<IProductAttributesAppService>();
            _productAttributeRepository = GetRequiredService<IRepository<ProductAttribute, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _productAttributesAppService.GetListAsync(new GetProductAttributesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("377d6a4f-2571-4641-9ad3-2590585fa6a6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _productAttributesAppService.GetAsync(Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProductAttributeCreateDto
            {
                AttrNo = 18,
                AttrName = "6f09226e346f466dbec9950fc136c70f149bf70b0491461f96901023d0c9a1c4ef9400d17f394e6687a37be85bfc5bd739bb",
                HierarchyLevel = 12,
                Active = true,
                IsProductCategory = true
            };

            // Act
            var serviceResult = await _productAttributesAppService.CreateAsync(input);

            // Assert
            var result = await _productAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(18);
            result.AttrName.ShouldBe("6f09226e346f466dbec9950fc136c70f149bf70b0491461f96901023d0c9a1c4ef9400d17f394e6687a37be85bfc5bd739bb");
            result.HierarchyLevel.ShouldBe(12);
            result.Active.ShouldBe(true);
            result.IsProductCategory.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductAttributeUpdateDto()
            {
                AttrNo = 12,
                AttrName = "0d03e1cb08034864af99b82c200b497113dde3baf1af46f7b20f15c788cea412e388b398f0494b398eeef0410887a4f9cd5f",
                HierarchyLevel = 5,
                Active = true,
                IsProductCategory = true
            };

            // Act
            var serviceResult = await _productAttributesAppService.UpdateAsync(Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"), input);

            // Assert
            var result = await _productAttributeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrNo.ShouldBe(12);
            result.AttrName.ShouldBe("0d03e1cb08034864af99b82c200b497113dde3baf1af46f7b20f15c788cea412e388b398f0494b398eeef0410887a4f9cd5f");
            result.HierarchyLevel.ShouldBe(5);
            result.Active.ShouldBe(true);
            result.IsProductCategory.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _productAttributesAppService.DeleteAsync(Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"));

            // Assert
            var result = await _productAttributeRepository.FindAsync(c => c.Id == Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"));

            result.ShouldBeNull();
        }
    }
}