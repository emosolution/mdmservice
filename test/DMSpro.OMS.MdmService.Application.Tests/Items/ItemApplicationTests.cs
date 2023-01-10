using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Items
{
    public class ItemsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemsAppService _itemsAppService;
        private readonly IRepository<Item, Guid> _itemRepository;

        public ItemsAppServiceTests()
        {
            _itemsAppService = GetRequiredService<IItemsAppService>();
            _itemRepository = GetRequiredService<IRepository<Item, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemsAppService.GetListAsync(new GetItemsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Item.Id == Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")).ShouldBe(true);
            result.Items.Any(x => x.Item.Id == Guid.Parse("97499669-1c9a-41d3-b607-5e14e66c687c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemsAppService.GetAsync(Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemCreateDto
            {
                Code = "1657a6c4724a4889a087",
                Name = "79c0586ba36241ba9668b798058cf2fc2518f479b76a4d69a5f5c87e69326ac52ce30a6f00404d109f9d570e476c7157a0a5aca53680499fb1f4e1bfbb529e64b2be7e634df144689bfded321ef188fe62421b354c65477d85ec838f430b24401bce6612d403457a84ad2c3f550075175bea7f2e7a134b48bc1ebd545a2dc9e",
                ShortName = "d2ae2651221841d9995851b9efa0b6500cd43d54d10a4440ade4af6b775c5930236b4200081a4c20af233a3d6e4e0fb2ef70ab1b9c4d47ccb1f473a79e275498f5abf96c6fe9444280c1bc2ea184730794b2c8f9eba941079d1619e4e48d0b7807dbd72b727c4516a79ab072ca5b532f9c43513b8221472b96fcfebb529edfc",
                ERPCode = "9ebb42a5e5f94968a925",
                Barcode = "8dda0c1eb1724153b394e58c6b52de0fb64761a990884d26a9",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 1385439330,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 723655992,
                IssueMethod = default,
                CanUpdate = true,
                ItemTypeId = Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                VatId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UomGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                PurUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                SalesUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),

            };

            // Act
            var serviceResult = await _itemsAppService.CreateAsync(input);

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("1657a6c4724a4889a087");
            result.Name.ShouldBe("79c0586ba36241ba9668b798058cf2fc2518f479b76a4d69a5f5c87e69326ac52ce30a6f00404d109f9d570e476c7157a0a5aca53680499fb1f4e1bfbb529e64b2be7e634df144689bfded321ef188fe62421b354c65477d85ec838f430b24401bce6612d403457a84ad2c3f550075175bea7f2e7a134b48bc1ebd545a2dc9e");
            result.ShortName.ShouldBe("d2ae2651221841d9995851b9efa0b6500cd43d54d10a4440ade4af6b775c5930236b4200081a4c20af233a3d6e4e0fb2ef70ab1b9c4d47ccb1f473a79e275498f5abf96c6fe9444280c1bc2ea184730794b2c8f9eba941079d1619e4e48d0b7807dbd72b727c4516a79ab072ca5b532f9c43513b8221472b96fcfebb529edfc");
            result.ERPCode.ShouldBe("9ebb42a5e5f94968a925");
            result.Barcode.ShouldBe("8dda0c1eb1724153b394e58c6b52de0fb64761a990884d26a9");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(1385439330);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(723655992);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemUpdateDto()
            {
                Code = "9a3718bfabd74f6e92e1",
                Name = "491af10f61224e9881f7350cee40413f224f759698744b4f95ca6d606bc3c18cb5a1102f3b564ddabb5fe7073d697a9218446b2ea0154cd0836501fe22c5fea95e2d440f6d2445c3b5b264e00104657667e9945895e3440faf740eec6422bb935477f78ba5d740f2990f5d07c6f8f4c7b2c516cffde44e57ab9596efdfc98c9",
                ShortName = "d4bc9202d497460e8545221097e7e314d3320ac362cf40348466ba3dee2cb2408b1e0546ec974511810a609608e661e8d02b8d302f4a4a5a91a10ceda2b53d73ace4d7731f394b31960d0f5031c6463f7a734a37154e41419b7336ba4a57ff320f64a5596c964b94984f1a977235e545729fe0d19dd04f4c971afafb1782e32",
                ERPCode = "8adae6f9429e4c808844",
                Barcode = "4eb2aecc561b466580c64e9329cd4c63d2656f74f4474ec8a6",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 2029155575,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 502601942,
                IssueMethod = default,
                CanUpdate = true,
                ItemTypeId = Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                VatId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UomGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                PurUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                SalesUOMId = Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),

            };

            // Act
            var serviceResult = await _itemsAppService.UpdateAsync(Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"), input);

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("9a3718bfabd74f6e92e1");
            result.Name.ShouldBe("491af10f61224e9881f7350cee40413f224f759698744b4f95ca6d606bc3c18cb5a1102f3b564ddabb5fe7073d697a9218446b2ea0154cd0836501fe22c5fea95e2d440f6d2445c3b5b264e00104657667e9945895e3440faf740eec6422bb935477f78ba5d740f2990f5d07c6f8f4c7b2c516cffde44e57ab9596efdfc98c9");
            result.ShortName.ShouldBe("d4bc9202d497460e8545221097e7e314d3320ac362cf40348466ba3dee2cb2408b1e0546ec974511810a609608e661e8d02b8d302f4a4a5a91a10ceda2b53d73ace4d7731f394b31960d0f5031c6463f7a734a37154e41419b7336ba4a57ff320f64a5596c964b94984f1a977235e545729fe0d19dd04f4c971afafb1782e32");
            result.ERPCode.ShouldBe("8adae6f9429e4c808844");
            result.Barcode.ShouldBe("4eb2aecc561b466580c64e9329cd4c63d2656f74f4474ec8a6");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(2029155575);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(502601942);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemsAppService.DeleteAsync(Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"));

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5"));

            result.ShouldBeNull();
        }
    }
}