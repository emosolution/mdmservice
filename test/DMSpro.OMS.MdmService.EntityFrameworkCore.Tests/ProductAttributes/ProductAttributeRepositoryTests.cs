using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IProductAttributeRepository _productAttributeRepository;

        public ProductAttributeRepositoryTests()
        {
            _productAttributeRepository = GetRequiredService<IProductAttributeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productAttributeRepository.GetListAsync(
                    attrName: "36fe4c206296495aa5cf2fe33a195721c0b24c92c337491085ec5d80ac7f39ff1a947e20f9ae498d8c5f1cf346244aed95c2",
                    active: true,
                    isProductCategory: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productAttributeRepository.GetCountAsync(
                    attrName: "be720ecb7bb84dfdbd39c1faabf861904cec6df44c314bbdb4887981012ff12b5732db15045942d4b3da404616489e4a5753",
                    active: true,
                    isProductCategory: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}