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
            result.Items.Any(x => x.Item.Id == Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4")).ShouldBe(true);
            result.Items.Any(x => x.Item.Id == Guid.Parse("ca66e65e-0340-47a1-a505-9351422ca037")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemsAppService.GetAsync(Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemCreateDto
            {
                Code = "88284ef0d5db4fcea8a1",
                Name = "59b63471d09a436c9950bd5875c2edb17610916af7b742b7a1e0c300fe7c9b14f9c17203495d4cb2aac3e784b2ad9367bf2691aa6a41403fa887fac5bdb7f4a8381f2c4d12f54c9ba882a1daf572845f8987573159444f258b00338c013525c669a2c49ba1f744cd9259bea1c6d6e6a0758af25df49540dfb8eeb88e44de79f",
                ShortName = "a766e703ae264927a26f4892b1ebff07cee71c871b864c948b5d8da645f0dd2869d6e24e0e0845acb99f90ba1cead10a3f271dcb394f45908dded5e5d4d94d1b2a9adb2f0c574bc69c5a09929d4c88efe841ffecc7ec449bb2709793fdb9c45f0dba7182374542fca6b42becc3b47bd0ece651e621ef4fbbbcde30183595784",
                erpCode = "c2ee9921b55c467faf88",
                Barcode = "f7580b6b59814c62a67b5181ea0b32a11b0db50c4e26451086",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 1118378735,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 261270654,
                IssueMethod = default,
                CanUpdate = true,
                PurUnitRate = 1522992531,
                SalesUnitRate = 283141640,
                ItemTypeId = Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                VatId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UomGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                PurUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                SalesUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),

            };

            // Act
            var serviceResult = await _itemsAppService.CreateAsync(input);

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("88284ef0d5db4fcea8a1");
            result.Name.ShouldBe("59b63471d09a436c9950bd5875c2edb17610916af7b742b7a1e0c300fe7c9b14f9c17203495d4cb2aac3e784b2ad9367bf2691aa6a41403fa887fac5bdb7f4a8381f2c4d12f54c9ba882a1daf572845f8987573159444f258b00338c013525c669a2c49ba1f744cd9259bea1c6d6e6a0758af25df49540dfb8eeb88e44de79f");
            result.ShortName.ShouldBe("a766e703ae264927a26f4892b1ebff07cee71c871b864c948b5d8da645f0dd2869d6e24e0e0845acb99f90ba1cead10a3f271dcb394f45908dded5e5d4d94d1b2a9adb2f0c574bc69c5a09929d4c88efe841ffecc7ec449bb2709793fdb9c45f0dba7182374542fca6b42becc3b47bd0ece651e621ef4fbbbcde30183595784");
            result.erpCode.ShouldBe("c2ee9921b55c467faf88");
            result.Barcode.ShouldBe("f7580b6b59814c62a67b5181ea0b32a11b0db50c4e26451086");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(1118378735);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(261270654);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.PurUnitRate.ShouldBe(1522992531);
            result.SalesUnitRate.ShouldBe(283141640);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemUpdateDto()
            {
                Code = "18cea4780bc84f8bb7e7",
                Name = "a2024d00fc75454f9a1fb1b5bc131d2e5bb4a345631e45cfb7e3958a4e527a90dbb7c59b66b84e9ba7b397ef407f8ed512bfe4fce8fd4610847cccf6a9c51a99d8f52eaca00d42f9a55c2dc2570d8caabccfb593a9cd48c3ad5a6f9978abf150c8c53a1ea2044469966862bb6b23d5f10ab7a6e8cea04abe8f345f2f579f777",
                ShortName = "516f1544a9d04fdcb33369f261d32777e0c979c641834a37aa435f422dff8a48b917ccc6ac63439b9e3eece2c061eb5d4e61d76fae584fd4a707e539cfa773b2f52b744c155547fab47311376f6e069a1e38f08226034b77a93a44430de4c687c3c00f0354d845fa929ad04976b73dcb1b2eee783f0640169ca68093434d6c2",
                erpCode = "92f4e2d95b8243c58ff8",
                Barcode = "2ac4670003e74ed7b935bc3544f2e1b3206fe4e9e51e47769e",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 78554204,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 499406014,
                IssueMethod = default,
                CanUpdate = true,
                PurUnitRate = 613278082,
                SalesUnitRate = 593255752,
                ItemTypeId = Guid.Parse("0769ab5b-e53d-4065-a948-55423dd223f7"),
                VatId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UomGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                PurUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                SalesUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),

            };

            // Act
            var serviceResult = await _itemsAppService.UpdateAsync(Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"), input);

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("18cea4780bc84f8bb7e7");
            result.Name.ShouldBe("a2024d00fc75454f9a1fb1b5bc131d2e5bb4a345631e45cfb7e3958a4e527a90dbb7c59b66b84e9ba7b397ef407f8ed512bfe4fce8fd4610847cccf6a9c51a99d8f52eaca00d42f9a55c2dc2570d8caabccfb593a9cd48c3ad5a6f9978abf150c8c53a1ea2044469966862bb6b23d5f10ab7a6e8cea04abe8f345f2f579f777");
            result.ShortName.ShouldBe("516f1544a9d04fdcb33369f261d32777e0c979c641834a37aa435f422dff8a48b917ccc6ac63439b9e3eece2c061eb5d4e61d76fae584fd4a707e539cfa773b2f52b744c155547fab47311376f6e069a1e38f08226034b77a93a44430de4c687c3c00f0354d845fa929ad04976b73dcb1b2eee783f0640169ca68093434d6c2");
            result.erpCode.ShouldBe("92f4e2d95b8243c58ff8");
            result.Barcode.ShouldBe("2ac4670003e74ed7b935bc3544f2e1b3206fe4e9e51e47769e");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(78554204);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(499406014);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.PurUnitRate.ShouldBe(613278082);
            result.SalesUnitRate.ShouldBe(593255752);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemsAppService.DeleteAsync(Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"));

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == Guid.Parse("b84be110-0545-45ad-8195-2bf93d4dcec4"));

            result.ShouldBeNull();
        }
    }
}