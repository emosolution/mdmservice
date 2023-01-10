using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemImageRepository _itemImageRepository;

        public ItemImageRepositoryTests()
        {
            _itemImageRepository = GetRequiredService<IItemImageRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemImageRepository.GetListAsync(
                    description: "3836f282653e468ea6a9f04a1faa5d5768274d1809f142d0b3063febc8ab4693060fcd51a5504dfd861f3b92f2a803d88bf8ed0362014a2ca4a75a0352f2744b4d74b7d5b27d4606ba7d56e04c631d910444f7995c4b42f98080ae97e9ad8b4c49accd69075c4db89030a197d00ef609542e1b39f11c47efb141ec266d80173fe3fb8a40b07c4674bb59015eed98767635267fd317be4ebfba9d555fb48eb7e47dfe465e0c3f4c269f00c9fd835993823def0ccf2ded47209c78374dbbb636d62198f2c6557c49adaeee4568e696b2827e53116440a740eeb691b1597976fa86214d2e917c234f4d9a207139e4f08451fa5935a3a9284308aedd",
                    url: "a191de41b79747bfa3d15a81c36983e5a6a1ff4c8ac840d98b4514e5bc77a6a59b8994cd85be4f6cb973a1b1e7bfe86751572aa76cd9477dac505bff46b725dba07f8bc6f044423f9939aee1b0aec0e260bf7a8eeb9e4b39b523387278bb50ffe02b971267534948b2c1a8700bd4b1c8948a5c01ca74450aa83af0fd78bd82981943fe426d5c4281baa4ab889ace83ba81758d0fbbb14e9a8f0daa486bdb429e68f522d2ae2c413caa41907eba2dc2cb188ccf48fd434d1485c79f282665dede1105b7d4224344c689e4a386ab4028decc6772bbe97f49d8bd9c7c1242595ddb6d02c066bd74465b88e9a00891945d61506e6193657541b88186",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ac84691f-4f55-405b-b946-9f9eea7e761f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemImageRepository.GetCountAsync(
                    description: "2883babc11534fab8df85f515607b0a9643b854827184622bd2413030b191e5664be609dfa9b44e88667bc6ab5c47e5ebaebdc37bf874263a9c3ee974ba525f35330aec2423a48ad89b895c76a8824b5e16cd2e172d44249988b54259e3f123ddb21dcfae90647de8393884d748f71b47329d1a9ff024107a6a628de512a67a1ac89123c337a43dc88acc33e5ce6999d8002d6faa98745c9aab034dbdf50449cd8e2dd63f18c443f9136cd859fc0b2663cf8fbf7e08f4b108bf97b048e0766dce37646064cec496c87f79c0729cad01af3b6ce90a74948c2870b9070ba93f0f5e1d6250cd748479588687010294aac82b87b59286e774730b3ce",
                    url: "12ae8bcc345f41a0a4323bbbb32a4f3ec065403ce54841ce8a4dd54883f2c39cc0e07b49048a4ea8888270462048590c1246e68bdd61447dbf2ea4350c0bf1a3b36e3ec6dd5148fa882b4d8b9f55bbdcdc4aa5d3486446a9975e787826e2337cfe806e769d08464d96f1e753d49e7aaa794102bdd5e64f8292999a9b539edf56643a1ea9e2404bfe8721d0a8709bb44edde6253c509442d1ab59ab3e9297c77e7323278e6e1b4a67a700ac7aa5add3ba9287729ef40e4c349e90f00229c9120aa883a8fc8f6b41189582d90ebb5e8c51af26a7f9fdf1402b93200bdd5590a98eb0008adee7094c3b97a497328546c39c9821a10b66a94af09333",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}