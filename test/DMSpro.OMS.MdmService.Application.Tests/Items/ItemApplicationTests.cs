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
        public async Task GetAsync()
        {
            // Act
            var result = await _itemsAppService.GetAsync(Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemCreateDto
            {
                Name = "4792f9f6be354004aa0e09e7d71cd4af18c3dc7cbf1c436483e04f56bf1dd546ad897dd2014e46be9df66a7accf8da6a83d6f447393344a7abac56e9c084ac047979ce8ce08b41cf9b593e6894964d029d9b925d1d95421c90ea30dcdab069f4c1d572f243a64c9d90a71a17bf1af331a0920c3f93384409b4e5c76cfc1f620",
                ShortName = "d5864a7f38ea4b2ea7dbf147e26e1d87979773c3bf14483e8a2e26a6cd63ed99a5ae5dcfb6e64f0297cde641a2d43523a64bb26b640a4114b779b727d04eb458b443372c380745deaf5740ea520e7832d36f943bd3fa4a8a8c1ab8df37b0637b47b150c13f774997a8564a91d3bfcacb588ac28a451d40c4ba8022df15f9808",
                erpCode = "880d5e326822489f82ea",
                Barcode = "e775b959674142aead61bb7c026252ceabdb129192784dc9b8",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 2125583594,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 1857327589,
                IssueMethod = default,
                CanUpdate = true,
                PurUnitRate = 806122061,
                SalesUnitRate = 989201396,
                ItemType = default,
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
            result.Name.ShouldBe("4792f9f6be354004aa0e09e7d71cd4af18c3dc7cbf1c436483e04f56bf1dd546ad897dd2014e46be9df66a7accf8da6a83d6f447393344a7abac56e9c084ac047979ce8ce08b41cf9b593e6894964d029d9b925d1d95421c90ea30dcdab069f4c1d572f243a64c9d90a71a17bf1af331a0920c3f93384409b4e5c76cfc1f620");
            result.ShortName.ShouldBe("d5864a7f38ea4b2ea7dbf147e26e1d87979773c3bf14483e8a2e26a6cd63ed99a5ae5dcfb6e64f0297cde641a2d43523a64bb26b640a4114b779b727d04eb458b443372c380745deaf5740ea520e7832d36f943bd3fa4a8a8c1ab8df37b0637b47b150c13f774997a8564a91d3bfcacb588ac28a451d40c4ba8022df15f9808");
            result.erpCode.ShouldBe("880d5e326822489f82ea");
            result.Barcode.ShouldBe("e775b959674142aead61bb7c026252ceabdb129192784dc9b8");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(2125583594);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(1857327589);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.PurUnitRate.ShouldBe(806122061);
            result.SalesUnitRate.ShouldBe(989201396);
            result.ItemType.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemUpdateDto()
            {
                Name = "ffa88d332cd24a9a8236370e61eb15c84aaa6ab7ea524357a3e19da26e02c722ac27b5b73afa4086aae5dd947f8590c9330bf486e5e449bdb679eddc2b594c75412c5e43378c4fc6a25b506530ceaee08e9289db9a5c4252ab9b5c3571e0ac0fd8dde9f0b15440df9385d9776840385f4e4abd291d984167a84cce8ab29652c",
                ShortName = "af15b8d264664eb19454e9aec51c60b23849cd5ef06b4a3580ec51d50d17f87052c15eaac42a4494bf2c6c27988b48dd3e9b31b1eb8840859ed881526a101f1af75c613dd0394896897cb5c332259cb22b36f1c5632f4b7a8202ec4b9723489914bb113a870e4c96922797a861d4577780c88c33ff344422876bf86074462fb",
                erpCode = "e9f86761fdb44d2eb76e",
                Barcode = "a72dac0dffdf448da54b50bff5c535bfd6b36a45bca84ad29d",
                IsPurchasable = true,
                IsSaleable = true,
                IsInventoriable = true,
                BasePrice = 1637301533,
                Active = true,
                ManageItemBy = default,
                ExpiredType = default,
                ExpiredValue = 2030144152,
                IssueMethod = default,
                CanUpdate = true,
                PurUnitRate = 1407412368,
                SalesUnitRate = 1775138598,
                ItemType = default,
                VatId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UomGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                PurUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                SalesUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),

            };

            // Act
            var serviceResult = await _itemsAppService.UpdateAsync(Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"), input);

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("ffa88d332cd24a9a8236370e61eb15c84aaa6ab7ea524357a3e19da26e02c722ac27b5b73afa4086aae5dd947f8590c9330bf486e5e449bdb679eddc2b594c75412c5e43378c4fc6a25b506530ceaee08e9289db9a5c4252ab9b5c3571e0ac0fd8dde9f0b15440df9385d9776840385f4e4abd291d984167a84cce8ab29652c");
            result.ShortName.ShouldBe("af15b8d264664eb19454e9aec51c60b23849cd5ef06b4a3580ec51d50d17f87052c15eaac42a4494bf2c6c27988b48dd3e9b31b1eb8840859ed881526a101f1af75c613dd0394896897cb5c332259cb22b36f1c5632f4b7a8202ec4b9723489914bb113a870e4c96922797a861d4577780c88c33ff344422876bf86074462fb");
            result.erpCode.ShouldBe("e9f86761fdb44d2eb76e");
            result.Barcode.ShouldBe("a72dac0dffdf448da54b50bff5c535bfd6b36a45bca84ad29d");
            result.IsPurchasable.ShouldBe(true);
            result.IsSaleable.ShouldBe(true);
            result.IsInventoriable.ShouldBe(true);
            result.BasePrice.ShouldBe(1637301533);
            result.Active.ShouldBe(true);
            result.ManageItemBy.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(2030144152);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.PurUnitRate.ShouldBe(1407412368);
            result.SalesUnitRate.ShouldBe(1775138598);
            result.ItemType.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemsAppService.DeleteAsync(Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"));

            // Assert
            var result = await _itemRepository.FindAsync(c => c.Id == Guid.Parse("43ba1fc6-3f5a-436d-9757-80984aec30fa"));

            result.ShouldBeNull();
        }
    }
}