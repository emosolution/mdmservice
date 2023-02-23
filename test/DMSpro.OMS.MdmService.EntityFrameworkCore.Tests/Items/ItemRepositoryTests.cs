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
                    code: "0fb2857d1ace436e8d44",
                    name: "0c987874b116471bbd08d8cd316a4b4c355b5ee0b9bd4331bfaf3bba86c1907bf7d812f109de446bb30a97ac77d871469895c4ca2e904fbf8cab40aa0cb8253b99adf9fa2e8c4c8a98b77f9d61d12b97491d4a4f7e1e4e828ae8c9afd93049b22cee5663f5014dcc8fee3914aafa2767c520ebb20e6343459495af25af067c9",
                    shortName: "9bcac9b7363a473e96af98897b367f13b11022e68d2a48958b8a52dd959e3ad8ca6f95cded8c4c2383fa4706dc4defcf314c51275ad04426bc8ae1c2a4ca6263fec793f5daf14caa863772bb0228a2ec27f8d99c5d86446eb383806a3c13e3527bd54f743810464280b955f7a088ecc502b3141cce4548b89b3135beaeb02f7",
                    erpCode: "d42de760d4184242b684",
                    barcode: "18a9ac33b5a3407fa1934260ef3f6b26bae9e67b174546a6ae",
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
                result.First().Id.ShouldBe(Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"));
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
                    code: "d13b0ba482c64ef6ae9e",
                    name: "0fbd46d477f844749ed9b5180165c37781619646554c438e9bf32d18ee51800604117ff27b4c446fbe9025ddeed5003624c88bb05fe54dd9b812bf987f6fe4a93939ea411b8542ae9b0b0ba4106f0498797c3070c0b344d9b260dc6a525cf375c7598da75d2e4b2baa0cd0bccaa5cb7d79991f96271b41db9925ce9481ea44e",
                    shortName: "f1b9c2f5ea474e898bcdaa9d42d0f6b3a0862b74e5874a19977c04e9c4107e6e401f9cc39d18434bab57744698391e46cc123fa63eea4b25a35e4ed6a42ea7e6d13e622170ea43b69ed334e977b8513b44764c905b2643068e60b55ebbe59d4f1f4a9f8d04d1498a8ca8d44f3bac478325d2eaa56e914ba68e1b2115302e424",
                    erpCode: "e926997da7b3417b9f27",
                    barcode: "bd7e23dd8f834673876fcfe0da21d982cd3033f66b8e41e6a5",
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