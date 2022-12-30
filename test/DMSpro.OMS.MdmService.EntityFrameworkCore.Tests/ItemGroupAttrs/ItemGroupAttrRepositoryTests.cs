using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroupAttrs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttrRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupAttrRepository _itemGroupAttrRepository;

        public ItemGroupAttrRepositoryTests()
        {
            _itemGroupAttrRepository = GetRequiredService<IItemGroupAttrRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupAttrRepository.GetListAsync(
                    dummy: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupAttrRepository.GetCountAsync(
                    dummy: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}