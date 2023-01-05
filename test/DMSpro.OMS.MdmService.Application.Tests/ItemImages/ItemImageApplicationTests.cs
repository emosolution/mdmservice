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
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f")).ShouldBe(true);
            result.Items.Any(x => x.ItemImage.Id == Guid.Parse("4c1561df-fcc3-4df7-ba24-5c5bb0c6e391")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemImagesAppService.GetAsync(Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemImageCreateDto
            {
                Description = "a06278eac38f4119b5901d88c90a42f14fbfd96b69a141e48e91400893087368b81508c59af64ef5a8581c61280bb4c12a059230a31243f0b6995e2db530850a9fa02c779fdf4b72ad104e01bd23c323a3d0dc2042aa410b92d079541a7ada39d9d4feb8014b4f93a98bee92fa387aec182a5c919c8a4deda49c8d4e77aeb40fc0a93369e13b44b3947b446bdbf20aeb909c970a6f6c46d49468cb129138f109d7f81f84daeb4712be91120fbd2b88c01136b39ecd5144af8437996c7b6367a38817eb5a5c5941e3ac7f96b2bc48b033ce868e36423b4788b438e4f9acd2695e92c72de5b4e44f349d55b8d2eda36c4931e2931026724bbd85a9",
                Url = "6d87d4e92eb6494dab33c7aa7ef8a3ae20cb05f0958a4e85a6d821e74a34fe9038c7916d32b64000ba31ff8fdd8a67c22450a40e3f764567a39004f01fe13426a01cd3316ea2481697a806e67ef3d7f262d0e390513049fb8ebb6ce81cae4a526bede6bcccd842b983e3f0419bba54ff08c02935a7b14049bbc7cc165241489a0d7f78c681c34d71bcb0cfe1e39a1904f085700112d64071aad67f215fe85ccb5008337ecdc84a6fb8c1bd52ac3077749568642174154c5fbec480f14f0ee30553a4dad5eb20413aaa6f7c1541e8cc457631825e87684c50af0f958bd2e4c57957622aec59094dc795111f2d95fe4a1c2985f832821440d69a31",
                Active = true,
                DisplayOrder = 641132537,
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemImagesAppService.CreateAsync(input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("a06278eac38f4119b5901d88c90a42f14fbfd96b69a141e48e91400893087368b81508c59af64ef5a8581c61280bb4c12a059230a31243f0b6995e2db530850a9fa02c779fdf4b72ad104e01bd23c323a3d0dc2042aa410b92d079541a7ada39d9d4feb8014b4f93a98bee92fa387aec182a5c919c8a4deda49c8d4e77aeb40fc0a93369e13b44b3947b446bdbf20aeb909c970a6f6c46d49468cb129138f109d7f81f84daeb4712be91120fbd2b88c01136b39ecd5144af8437996c7b6367a38817eb5a5c5941e3ac7f96b2bc48b033ce868e36423b4788b438e4f9acd2695e92c72de5b4e44f349d55b8d2eda36c4931e2931026724bbd85a9");
            result.Url.ShouldBe("6d87d4e92eb6494dab33c7aa7ef8a3ae20cb05f0958a4e85a6d821e74a34fe9038c7916d32b64000ba31ff8fdd8a67c22450a40e3f764567a39004f01fe13426a01cd3316ea2481697a806e67ef3d7f262d0e390513049fb8ebb6ce81cae4a526bede6bcccd842b983e3f0419bba54ff08c02935a7b14049bbc7cc165241489a0d7f78c681c34d71bcb0cfe1e39a1904f085700112d64071aad67f215fe85ccb5008337ecdc84a6fb8c1bd52ac3077749568642174154c5fbec480f14f0ee30553a4dad5eb20413aaa6f7c1541e8cc457631825e87684c50af0f958bd2e4c57957622aec59094dc795111f2d95fe4a1c2985f832821440d69a31");
            result.Active.ShouldBe(true);
            result.DisplayOrder.ShouldBe(641132537);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemImageUpdateDto()
            {
                Description = "6e9cf9804aa3432b885952c4cdd53bee61e047cf27a149cfad331d5e2961a70283d5c7193d404c9086fc40ac0f6d1546a661b1d27d464044bcac5b8af18032786016762625d9415ca2e6b2f55dbbba9ef8f321bbbc5546e0817b2298fab1f73fc5ec2083019b4473b0cc0b69e8457f18a1cab9aaf63643138134c34e6891bd71a149f09cac21442585da698b1e1ca7cde1257e1794694767b9eca392cfbc039fe3b22c57acee4f23b06fc98a614618a8d857e2bf7582435eb8b9a011d2aeceb70b41c293a5824fdabdec0df9cb9423bcddb3ed2bedc7425da6c13b5dbee4c0e73451f2e03f58405eafb412e0a145ce1d7bf4f7248f934bb188cf",
                Url = "c36fce9d87ae4fb4b1c5741750beafd97d367f3021554e609944e106003ad3411ad48c98453348a18cbc6a07d6d7bb7f7bc53b36d7e148d2a00cade4c7d7336337efb7fbd3e0464a9a2d607c68bd8f080f4449021431477d8bc7ff0bc8d807efd84c5ef45e3f44839c667f5132e4225d725a6605b658439ea3004ad482f75bc18ff098de52a34d86a385cf9784780dc719486b8df4864092aa2c3c287a9f32539aa5cbc5f3884a90a0cc7c439a13f820ef4a02f085d8477a8dbdc977da9a269d62be175b434a44e186f2c3ab17ca39bce1eb8e55fa09455cbad79be03d9935d6a66991329c4f4cd288b22dc63f92e4b85cb6739be5c9468094f6",
                Active = true,
                DisplayOrder = 1650202217,
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemImagesAppService.UpdateAsync(Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"), input);

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("6e9cf9804aa3432b885952c4cdd53bee61e047cf27a149cfad331d5e2961a70283d5c7193d404c9086fc40ac0f6d1546a661b1d27d464044bcac5b8af18032786016762625d9415ca2e6b2f55dbbba9ef8f321bbbc5546e0817b2298fab1f73fc5ec2083019b4473b0cc0b69e8457f18a1cab9aaf63643138134c34e6891bd71a149f09cac21442585da698b1e1ca7cde1257e1794694767b9eca392cfbc039fe3b22c57acee4f23b06fc98a614618a8d857e2bf7582435eb8b9a011d2aeceb70b41c293a5824fdabdec0df9cb9423bcddb3ed2bedc7425da6c13b5dbee4c0e73451f2e03f58405eafb412e0a145ce1d7bf4f7248f934bb188cf");
            result.Url.ShouldBe("c36fce9d87ae4fb4b1c5741750beafd97d367f3021554e609944e106003ad3411ad48c98453348a18cbc6a07d6d7bb7f7bc53b36d7e148d2a00cade4c7d7336337efb7fbd3e0464a9a2d607c68bd8f080f4449021431477d8bc7ff0bc8d807efd84c5ef45e3f44839c667f5132e4225d725a6605b658439ea3004ad482f75bc18ff098de52a34d86a385cf9784780dc719486b8df4864092aa2c3c287a9f32539aa5cbc5f3884a90a0cc7c439a13f820ef4a02f085d8477a8dbdc977da9a269d62be175b434a44e186f2c3ab17ca39bce1eb8e55fa09455cbad79be03d9935d6a66991329c4f4cd288b22dc63f92e4b85cb6739be5c9468094f6");
            result.Active.ShouldBe(true);
            result.DisplayOrder.ShouldBe(1650202217);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemImagesAppService.DeleteAsync(Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"));

            // Assert
            var result = await _itemImageRepository.FindAsync(c => c.Id == Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"));

            result.ShouldBeNull();
        }
    }
}