using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValueRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;

        public CusAttributeValueRepositoryTests()
        {
            _cusAttributeValueRepository = GetRequiredService<ICusAttributeValueRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cusAttributeValueRepository.GetListAsync(
                    attrValName: "c3cf714f47274117b852c75a89de421aa76e42709b874776ad5ea2ce92ee13440d5af6f004f14aa18aafca8506631c261e46"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _cusAttributeValueRepository.GetCountAsync(
                    attrValName: "3fa7f94e627749eebfec64616286a7e19f56f7d3f2d64da9b911858eb796ede35cf1afd108064f989196c7fb10f173e73f47"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}