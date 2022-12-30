using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public class ItemGroupAttrsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemGroupAttrsAppService _itemGroupAttrsAppService;
        private readonly IRepository<ItemGroupAttr, Guid> _itemGroupAttrRepository;

        public ItemGroupAttrsAppServiceTests()
        {
            _itemGroupAttrsAppService = GetRequiredService<IItemGroupAttrsAppService>();
            _itemGroupAttrRepository = GetRequiredService<IRepository<ItemGroupAttr, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemGroupAttrsAppService.GetListAsync(new GetItemGroupAttrsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemGroupAttr.Id == Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7")).ShouldBe(true);
            result.Items.Any(x => x.ItemGroupAttr.Id == Guid.Parse("bf573342-35ca-4344-bac3-74cb3243c0f5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupAttrsAppService.GetAsync(Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupAttrCreateDto
            {
                Dummy = true,
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),

            };

            // Act
            var serviceResult = await _itemGroupAttrsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupAttrRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Dummy.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupAttrUpdateDto()
            {
                Dummy = true,
                ItemGroupId = Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"),

            };

            // Act
            var serviceResult = await _itemGroupAttrsAppService.UpdateAsync(Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"), input);

            // Assert
            var result = await _itemGroupAttrRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Dummy.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupAttrsAppService.DeleteAsync(Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"));

            // Assert
            var result = await _itemGroupAttrRepository.FindAsync(c => c.Id == Guid.Parse("31651b49-dec7-49e1-aa7d-344db29f1fa7"));

            result.ShouldBeNull();
        }
    }
}