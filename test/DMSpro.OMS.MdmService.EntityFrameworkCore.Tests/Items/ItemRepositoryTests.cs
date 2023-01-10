using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemRepositoryTests()
        {
            _itemRepository = GetRequiredService<IItemRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemRepository.GetListAsync(
                    code: "1976a166ea4846228637",
                    name: "65e2326954044b5e82715b2cb96387c50a889e2c42164747b4f8a65ec9e21e471240683096184168830482adda2c76212332466ba7844fe1b92b1df7f7d4ebaf15cdc6426ca547569cbc8e4fe14a682cb05981d6bb73495a9d5582adb0514f0c90f8cb6206d1444fbdf47c67d947ef0ea1b30bb4512f4fe793a85962b299e6e",
                    shortName: "34a35ed4bba44b4aa9c0eafccc3e715c91339f474c664279bc22ce976fc46cf205ba4fef8316478d9ef7fcdb5c3eea4bfcd5fda0d53242418d486f7dd6268f819fb6bc449e6d4b79b470c4d081d8f1a6a25bc7f623bf4a799b6ef36873ca51caa69c5d109e644e199279b82ff9a343ed633b1dd4b2534009be6418f7533fa4d",
                    eRPCode: "1ea8d46dacb441468efd",
                    barcode: "8d906e562f0c42c28899e349f4b003951943a61229d64c5b9f",
                    isPurchasable: true,
                    isSaleable: true,
                    isInventoriable: true,
                    active: true,
                    manageItemBy: default,
                    expiredType: default,
                    issueMethod: default,
                    canUpdate: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemRepository.GetCountAsync(
                    code: "3951c184e472413fbf6e",
                    name: "9408fb7368404d319cef1f6ae081f208b8eb623d9ee5420da3712ef84d5def0d6b92d31f750141788e46848489c9004514a8878d3e0a4c03a0a80e60efe601296008f3cd527644f695cecd20cb2c201c87aa9c3ae1f8484fac430a39224cf806fc7cd95bc90b42869e98d48d178b5b06fd62f44c32894a92aa5e8daedc634ce",
                    shortName: "928a17680baf4f3088a0ccb1da4d3e152b945a05c2ba4ccca320b4648a2ca5a0a7075976176f4d228acd9e0863046b873d8beb293874405592e1bc346ed8afbd0000683de87146ba8c0716dac785c9416f6892b5182d4c89b66b858097dd7e2276dde476594843a1b77fa0ae221e847737f012efd79d4e34adc4b2cc340f70a",
                    eRPCode: "30236ccf626f43fa9140",
                    barcode: "51f665011cbb49fbb28571654985104032b16f4732464aec89",
                    isPurchasable: true,
                    isSaleable: true,
                    isInventoriable: true,
                    active: true,
                    manageItemBy: default,
                    expiredType: default,
                    issueMethod: default,
                    canUpdate: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}