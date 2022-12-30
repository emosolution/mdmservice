using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMastersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemMastersAppService _itemMastersAppService;
        private readonly IRepository<ItemMaster, Guid> _itemMasterRepository;

        public ItemMastersAppServiceTests()
        {
            _itemMastersAppService = GetRequiredService<IItemMastersAppService>();
            _itemMasterRepository = GetRequiredService<IRepository<ItemMaster, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemMastersAppService.GetListAsync(new GetItemMastersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemMaster.Id == Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464")).ShouldBe(true);
            result.Items.Any(x => x.ItemMaster.Id == Guid.Parse("79ff2554-f670-4461-9400-fbc109666b2a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemMastersAppService.GetAsync(Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemMasterCreateDto
            {
                Code = "9b9254fa4e6f447f8de0",
                Name = "f6e74af0e10e4902bd191dc7412312c117464c55903a42048b8fc29f11c546da7a6b9344731f4043afdbf5f258de70655229a16564174da183a718b4f27d3888a6f7b14123ca4929b65b09ef77347900a6f62f09bb064c44ac1a17025ba3abf2c0c9f7d98498462f84a4868fc5296f6ce2d662dfcb8d411e93a9b14fbb29ade",
                ShortName = "11549fa0143c4a929f4ca0a53929b553ac28ee3c28ff46749423715a7e66a736f64c0075f5764050bd70d686f6ac6d5fb96b8aec7b214ee6b57b98071e2a2827108d190f16b5490b893c7326f4433ab7ae0d9af042f84199ad82278e2e1a59f7edce00fb7c13438384a5f8e3d5d65d8009d828e00c394a5e923ebdddff59cf5",
                ERPCode = "b05e44d02f984e00b2b5d5a9338e58ca7bfbb98ec74048ab8c3d730889067d076deccb4f7dd14cb485eabd364f6145a4bdff120a1a844f25a2c4a1bbd365970929a8bfa052b043beb303044036ee386539348fd5670d40c99a7398d365fca5358a30f97253134feb896457fe968ba2e1efa6041f5ee54445a7537c3c2b70223",
                Barcode = "0168db7765c6499498186b78efbb49f890d3ba850d3b4401bb01a47aac97caf2f6c2f26b8c5a4b12b324903de6a286acb1a20944124944c59005c28e15fb5e4e86f269845de641dfac7c0519226fc6bce482c88081704d438a994d6821cc9b36c24cd15a61104dbdb012329c4299e55a0cfcbc66ad9e4dd88d112497d5a94ad",
                Purchasble = true,
                Saleable = true,
                Inventoriable = true,
                Active = true,
                ManageType = default,
                ExpiredType = default,
                ExpiredValue = 1962829772,
                IssueMethod = default,
                CanUpdate = true,
                BasePrice = 1435590870,
                ItemTypeId = Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                VATId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UOMGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUnitId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                PurUnitId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                SalesUnit = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),

            };

            // Act
            var serviceResult = await _itemMastersAppService.CreateAsync(input);

            // Assert
            var result = await _itemMasterRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("9b9254fa4e6f447f8de0");
            result.Name.ShouldBe("f6e74af0e10e4902bd191dc7412312c117464c55903a42048b8fc29f11c546da7a6b9344731f4043afdbf5f258de70655229a16564174da183a718b4f27d3888a6f7b14123ca4929b65b09ef77347900a6f62f09bb064c44ac1a17025ba3abf2c0c9f7d98498462f84a4868fc5296f6ce2d662dfcb8d411e93a9b14fbb29ade");
            result.ShortName.ShouldBe("11549fa0143c4a929f4ca0a53929b553ac28ee3c28ff46749423715a7e66a736f64c0075f5764050bd70d686f6ac6d5fb96b8aec7b214ee6b57b98071e2a2827108d190f16b5490b893c7326f4433ab7ae0d9af042f84199ad82278e2e1a59f7edce00fb7c13438384a5f8e3d5d65d8009d828e00c394a5e923ebdddff59cf5");
            result.ERPCode.ShouldBe("b05e44d02f984e00b2b5d5a9338e58ca7bfbb98ec74048ab8c3d730889067d076deccb4f7dd14cb485eabd364f6145a4bdff120a1a844f25a2c4a1bbd365970929a8bfa052b043beb303044036ee386539348fd5670d40c99a7398d365fca5358a30f97253134feb896457fe968ba2e1efa6041f5ee54445a7537c3c2b70223");
            result.Barcode.ShouldBe("0168db7765c6499498186b78efbb49f890d3ba850d3b4401bb01a47aac97caf2f6c2f26b8c5a4b12b324903de6a286acb1a20944124944c59005c28e15fb5e4e86f269845de641dfac7c0519226fc6bce482c88081704d438a994d6821cc9b36c24cd15a61104dbdb012329c4299e55a0cfcbc66ad9e4dd88d112497d5a94ad");
            result.Purchasble.ShouldBe(true);
            result.Saleable.ShouldBe(true);
            result.Inventoriable.ShouldBe(true);
            result.Active.ShouldBe(true);
            result.ManageType.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(1962829772);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.BasePrice.ShouldBe(1435590870);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemMasterUpdateDto()
            {
                Code = "54da10c6962843dca3fa",
                Name = "8095639403db43a4894bda4382119d619bbbeecc2ab041cb97ab89d50b05c030b11178c458a2493f86b490e74ea3e154a20fca804a8a4ed0bb4f078b0afc6d144a82eab2b5ae4d3da7ad83ee05dfe3507b69458a6293436d954c22217287f2637d14e028d7ce48bfb18fe0ad9c20c96d2e7695a437cc4a9ab2631d875a643b6",
                ShortName = "654c8f1eb2e84bf8ba6a1b47933ec94f74aee8f71b8e47daaf2e31abbb5cdec49263235c663940deaed23de1403673bb2179f5d2437e46c7a934663d3279393f5231351ffdea4f13a4ee533ebf7bdeb84cba19043f5d44efa75c5bd78e19401a10108697b9d54072bce1781d3cb3996e1c7331c9da87409fa248d7dd1ff9aa7",
                ERPCode = "035ff30621e3415eb1546f32c3b3614b2aacab878ebb4e27aad17b1ad70d25fa8197173ad06b4a518162a8c0b466cd9ec094b36b8b0d4d33a5bee02e9045b8de4130620b7c374955a5bbd6a09a6eb7a27f5dad783df4436ea8ef9eb16a963db7dbcc25ee542547ff8bb25a5fdae80b6f41587f95e6f0485999a7475e37c0d32",
                Barcode = "2749ae82fef1436fa91582bd80feca4cfecb23415cec4cfaaf12116a86fde278f019bdda21b94f5cbaa10cfe7bdda6b6501ac40dc8554330878f92ab37d0f25bfbe13219f3ac4a26a0a6a375b7f30fc319727cb63de8444d8e49689b19d60ebd69a9a18ebb2148988fb3230fc3bcb3fd1912520a520b4c4188da7522bc55103",
                Purchasble = true,
                Saleable = true,
                Inventoriable = true,
                Active = true,
                ManageType = default,
                ExpiredType = default,
                ExpiredValue = 1192145588,
                IssueMethod = default,
                CanUpdate = true,
                BasePrice = 1404103288,
                ItemTypeId = Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                VATId = Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                UOMGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                InventoryUnitId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                PurUnitId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                SalesUnit = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),

            };

            // Act
            var serviceResult = await _itemMastersAppService.UpdateAsync(Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"), input);

            // Assert
            var result = await _itemMasterRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("54da10c6962843dca3fa");
            result.Name.ShouldBe("8095639403db43a4894bda4382119d619bbbeecc2ab041cb97ab89d50b05c030b11178c458a2493f86b490e74ea3e154a20fca804a8a4ed0bb4f078b0afc6d144a82eab2b5ae4d3da7ad83ee05dfe3507b69458a6293436d954c22217287f2637d14e028d7ce48bfb18fe0ad9c20c96d2e7695a437cc4a9ab2631d875a643b6");
            result.ShortName.ShouldBe("654c8f1eb2e84bf8ba6a1b47933ec94f74aee8f71b8e47daaf2e31abbb5cdec49263235c663940deaed23de1403673bb2179f5d2437e46c7a934663d3279393f5231351ffdea4f13a4ee533ebf7bdeb84cba19043f5d44efa75c5bd78e19401a10108697b9d54072bce1781d3cb3996e1c7331c9da87409fa248d7dd1ff9aa7");
            result.ERPCode.ShouldBe("035ff30621e3415eb1546f32c3b3614b2aacab878ebb4e27aad17b1ad70d25fa8197173ad06b4a518162a8c0b466cd9ec094b36b8b0d4d33a5bee02e9045b8de4130620b7c374955a5bbd6a09a6eb7a27f5dad783df4436ea8ef9eb16a963db7dbcc25ee542547ff8bb25a5fdae80b6f41587f95e6f0485999a7475e37c0d32");
            result.Barcode.ShouldBe("2749ae82fef1436fa91582bd80feca4cfecb23415cec4cfaaf12116a86fde278f019bdda21b94f5cbaa10cfe7bdda6b6501ac40dc8554330878f92ab37d0f25bfbe13219f3ac4a26a0a6a375b7f30fc319727cb63de8444d8e49689b19d60ebd69a9a18ebb2148988fb3230fc3bcb3fd1912520a520b4c4188da7522bc55103");
            result.Purchasble.ShouldBe(true);
            result.Saleable.ShouldBe(true);
            result.Inventoriable.ShouldBe(true);
            result.Active.ShouldBe(true);
            result.ManageType.ShouldBe(default);
            result.ExpiredType.ShouldBe(default);
            result.ExpiredValue.ShouldBe(1192145588);
            result.IssueMethod.ShouldBe(default);
            result.CanUpdate.ShouldBe(true);
            result.BasePrice.ShouldBe(1404103288);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemMastersAppService.DeleteAsync(Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"));

            // Assert
            var result = await _itemMasterRepository.FindAsync(c => c.Id == Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"));

            result.ShouldBeNull();
        }
    }
}