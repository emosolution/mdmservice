using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImagesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemImagesAppService _itemImagesAppService;
        private readonly IRepository<ItemImage, Guid> _itemImageRepository;

        public ItemImagesAppServiceTests()
        {
            _itemImagesAppService = GetRequiredService<IItemImagesAppService>();
            _itemImageRepository = GetRequiredService<IRepository<ItemImage, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemImagesAppService.GetListAsync(new GetItemImagesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6")).ShouldBe(true);
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("c4802d68-4559-4852-b8f0-e399a2b5ba7c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemImagesAppService.GetAsync(Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemImageCreateDto
            {
                Description = "af659073c6db4009a800e681458bc",
                Active = true,
                URL = "222298c063814dc6bde39b13cc774f28717d2081d6f3436baedf6a31b41922174f5fdd7",
                DisplayOrder = 1703401793,
                ItemId = Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            };

            // Act
            var serviceResult = await _itemImagesAppService.CreateAsync(input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("af659073c6db4009a800e681458bc");
            result.Active.ShouldBe(true);
            result.URL.ShouldBe("222298c063814dc6bde39b13cc774f28717d2081d6f3436baedf6a31b41922174f5fdd7");
            result.DisplayOrder.ShouldBe(1703401793);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemImageUpdateDto()
            {
                Description = "9bcf79fb163f444ca76f7610e0ae44e49c9d7572139e4581a226db0da71",
                Active = true,
                URL = "014b5a838f064f18bf88e9bbd4ba5cec254892c9716e4021be58ec166bffc5033639792c6116461ca31498",
                DisplayOrder = 476550833,
                ItemId = Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            };

            // Act
            var serviceResult = await _itemImagesAppService.UpdateAsync(Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"), input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("9bcf79fb163f444ca76f7610e0ae44e49c9d7572139e4581a226db0da71");
            result.Active.ShouldBe(true);
            result.URL.ShouldBe("014b5a838f064f18bf88e9bbd4ba5cec254892c9716e4021be58ec166bffc5033639792c6116461ca31498");
            result.DisplayOrder.ShouldBe(476550833);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemImagesAppService.DeleteAsync(Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"));

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == Guid.Parse("cda3773d-9602-4a0e-b53e-7be1ba1f59f6"));

            result.ShouldBeNull();
        }
    }
}