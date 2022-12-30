using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IProdAttributeValueRepository _prodAttributeValueRepository;

        public ProdAttributeValueRepositoryTests()
        {
            _prodAttributeValueRepository = GetRequiredService<IProdAttributeValueRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _prodAttributeValueRepository.GetListAsync(
                    attrValName: "fa20e780362a476eb80a7c3304a7d9fc943c48253cb24af78aaaed5f65bee2826561110677f74d07b466a531c0099addb2c7"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _prodAttributeValueRepository.GetCountAsync(
                    attrValName: "fb1ff3d3a41a4b62a8652ba025af83c0b23298f4f8f94ea0aebf2c1d0708f675e129e7c97fbb4433832acf5f373af0fb96dc"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}