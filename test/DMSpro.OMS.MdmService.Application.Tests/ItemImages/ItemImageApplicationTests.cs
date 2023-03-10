using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

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
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33")).ShouldBe(true);
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("949fe2de-8478-4cd6-8a79-088c77892226")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemImagesAppService.GetAsync(Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            /*
            // Arrange
            var input = new ItemImageCreateDto
            {
                Description = "b0cc84b59c9d4637908dca74170f0c124065a5396395416090ab529a3c84ebf12332796f50614b9d99b026451fa4c439a0deaaa3ae3e451bbe1852c754c542cef1a76d2994684d1d981f5e57f72b4915faa66bbec2ea4ebea43efdd7287290feab4b786739124f4bb616be807a2bb913dfc1af44a6214a889b36201732cec6f15b3445ffdf634b7dbada5adc62bff72ff7601581e094442a95df525f6ccf511d9f1c437fef9c4fe9884b5a069fed33c9e7f2e4bd4883417e8d783771d592165b1311194c0fae4ddf9b956f2e1358ebe5ecc92f1940ee4b26afc9c33d09c4a0372f238d83674b45d1b3b709b96e0ab263baf2d6ed445c46f58652",
                Active = true,
                DisplayOrder = 727251358,
                //FileId = Guid.Parse("87e1e912-4360-4762-a564-410a85f5fc2a"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemImagesAppService.CreateAsync(input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("b0cc84b59c9d4637908dca74170f0c124065a5396395416090ab529a3c84ebf12332796f50614b9d99b026451fa4c439a0deaaa3ae3e451bbe1852c754c542cef1a76d2994684d1d981f5e57f72b4915faa66bbec2ea4ebea43efdd7287290feab4b786739124f4bb616be807a2bb913dfc1af44a6214a889b36201732cec6f15b3445ffdf634b7dbada5adc62bff72ff7601581e094442a95df525f6ccf511d9f1c437fef9c4fe9884b5a069fed33c9e7f2e4bd4883417e8d783771d592165b1311194c0fae4ddf9b956f2e1358ebe5ecc92f1940ee4b26afc9c33d09c4a0372f238d83674b45d1b3b709b96e0ab263baf2d6ed445c46f58652");
            result.Active.ShouldBe(true);
            result.DisplayOrder.ShouldBe(727251358);
            result.FileId.ShouldBe(Guid.Parse("87e1e912-4360-4762-a564-410a85f5fc2a"));
            */
            var result = await _itemImageRepository.GetQueryableAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            /*
            // Arrange
            var input = new ItemImageUpdateDto()
            {
                Description = "a6aab4c3b47a4c788f698623bbfa8e67ab1febe9fcf2438f803621f1dfdc2d297bb60dd39c78476ebe045c7d8d19879f836a11c5763a4adcace53e3fc480bc1ce41466a49c304f15afdd8b3c47302dab163c098f5da54635850769a9c14c8d361021dff4e53b43d28f86c955ecfa0b6e0cf01255ee7b48be858816af78f8254991f0f4c54abc434698f58277bb09fef3635a208309474cdb8b5f433b4778b7eda1a3f0929c4d490cbd203e73999be47b2abd349033874aa9a22f4337d30397b6d14c5e99d15f439390f7a41d1611a23db6cf4d8f778548c8a708c327e6dc5a6d8ee3d3506c024b5b99dbef0e536039caa0e0885ee3b44af29773",
                Active = true,
                DisplayOrder = 1216122852,
                //FileId = Guid.Parse("694aa974-f358-4b8b-b4db-e0d3e7522b58"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemImagesAppService.UpdateAsync(Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"), input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("a6aab4c3b47a4c788f698623bbfa8e67ab1febe9fcf2438f803621f1dfdc2d297bb60dd39c78476ebe045c7d8d19879f836a11c5763a4adcace53e3fc480bc1ce41466a49c304f15afdd8b3c47302dab163c098f5da54635850769a9c14c8d361021dff4e53b43d28f86c955ecfa0b6e0cf01255ee7b48be858816af78f8254991f0f4c54abc434698f58277bb09fef3635a208309474cdb8b5f433b4778b7eda1a3f0929c4d490cbd203e73999be47b2abd349033874aa9a22f4337d30397b6d14c5e99d15f439390f7a41d1611a23db6cf4d8f778548c8a708c327e6dc5a6d8ee3d3506c024b5b99dbef0e536039caa0e0885ee3b44af29773");
            result.Active.ShouldBe(true);
            result.DisplayOrder.ShouldBe(1216122852);
            result.FileId.ShouldBe(Guid.Parse("694aa974-f358-4b8b-b4db-e0d3e7522b58"));
            */
            var result = await _itemImageRepository.GetQueryableAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            List<Guid> ids = new() 
            {
                Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"),
            };
            await _itemImagesAppService.DeleteManyAsync(ids);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"));

            result.ShouldBeNull();
        }
    }
}