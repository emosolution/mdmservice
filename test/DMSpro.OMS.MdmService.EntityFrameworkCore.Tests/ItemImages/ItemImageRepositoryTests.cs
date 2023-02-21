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
                    description: "3902ec57ffba48a08bc61fe86bc6c919dd1beb3281f644f9b7cd0c2aa7ee1aeb7eb33e127b55404b94685db61f5fe0d9d94fca43bc1a4209913aa7f0e10ef1e8e46a7d6331184788a3c97f02828c1c3a41bb36d65a67404293d69eb00ec3120bb62ccaea0abd454b9656c3c45000c6fa0393cad3175a460c88f0a26d8e4d8962018a0b6ac840438ab09c60fb7110a0b9a484af69835e484c87f0e17cde72be68ed1d9626839e46959860e3f58a1fcd5026a2e96d50b74b6f8c9175a7de4268097640d99924884be09c623ed71695757f67c6c8a01fc64bc595b3ba4d87cc42d1bc9a22d8480844538c1b0cd32e8a89af60cb649dff74436897a7",
                    active: true,
                    fileId: Guid.Parse("54fb3006-0732-4e72-af2d-51d582d73fd7")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("bd66c0e4-7e66-4931-9c5a-20b2c8eeec33"));
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
                    description: "9daa5315272b4146b07151489f4682d00c5cb680487e44fdbfc4d6f8e47e01949c4dcfe745964c078a63bcbfd032701464802e38143d4aa38acc9001bbd01a5f539484c256be48dc8417703e8fae026093adaa3710914a9c931d1253260ffb939b98ccc94b0641d2bf086dfedc546745e8a986d12f7f41118292449f4293a6bb014bd049763e482e8bd35a334504002d9ece86e7741c4272a7edc95b14832c80c4f142ce7d4a49229dd1aeadce8b755ac61d4e00364d494f978011a7d3fe4ec4873c3855aa8844588d0ad50b96c55d8403b264dea649482d8549368c8f79314f789f16464d7b41989f768d58ddb0265a4cbea4113f664c10bd86",
                    active: true,
                    fileId: Guid.Parse("7e6060ad-33f5-4636-817e-b2e91c57f089")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}