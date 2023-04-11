using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemGroupsAppService _itemGroupsAppService;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;

        public ItemGroupsAppServiceTests()
        {
            _itemGroupsAppService = GetRequiredService<IItemGroupsAppService>();
            _itemGroupRepository = GetRequiredService<IRepository<ItemGroup, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupsAppService.GetAsync(Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupCreateDto
            {
                Code = "5927b565bbcd4b5084d5dcb0e5f09ec5bc0e9294b6fd43c5bb",
                Name = "b5342ee123224cbeaaab41865509c2c7252834236f864a73b43ee2685e2971b927f1c5240d43498fa59f1ce74fcbbb4783",
                Description = "44f177fdfb7541b18fb30c5e782e1a4e40332cacbd3a429886b1d162620d834931ddc6b7ae7a4391af64062628f8fc857666a8f83fb7418db9d0778587de104f6e7364f92592471e8cc8dbe9f836963049868d3ed4704db5a7db1ad6bb1c3bd98a7496967b33485d84b45b6dc76acf2bc94bc0939fa746afb24691e10c99337",
                Type = default,
                Status = default
            };

            // Act
            var serviceResult = await _itemGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("5927b565bbcd4b5084d5dcb0e5f09ec5bc0e9294b6fd43c5bb");
            result.Name.ShouldBe("b5342ee123224cbeaaab41865509c2c7252834236f864a73b43ee2685e2971b927f1c5240d43498fa59f1ce74fcbbb4783");
            result.Description.ShouldBe("44f177fdfb7541b18fb30c5e782e1a4e40332cacbd3a429886b1d162620d834931ddc6b7ae7a4391af64062628f8fc857666a8f83fb7418db9d0778587de104f6e7364f92592471e8cc8dbe9f836963049868d3ed4704db5a7db1ad6bb1c3bd98a7496967b33485d84b45b6dc76acf2bc94bc0939fa746afb24691e10c99337");
            result.Type.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupUpdateDto()
            {
                Code = "404489c5df7a4d2496e539fe6958264384cfc879a4eb4f36a7",
                Name = "51d0de1ae5be4dd484cee8f44fd775e4b606d10840a04b67abd82aae2eab8411017ad39e23d04d1abe81f",
                Description = "b31424916e134fb9956a90f274b7b1e0237e587159aa42b99b9b2491e0ecd41e79699d75bbd24f52b84e0807d400f8732390bbed8c6d4d60ab21d7c094354a33d29dd30cc24c4f11b5a82d0de55c0b3f735a4dc1205045d0847525ca0c061d86ae2a34d5094d4a1eb55acbda20257643694550910ab1481f8e5d87f67bafe4b",
                Type = default,
                Status = default
            };

            // Act
            var serviceResult = await _itemGroupsAppService.UpdateAsync(Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"), input);

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("404489c5df7a4d2496e539fe6958264384cfc879a4eb4f36a7");
            result.Name.ShouldBe("51d0de1ae5be4dd484cee8f44fd775e4b606d10840a04b67abd82aae2eab8411017ad39e23d04d1abe81f");
            result.Description.ShouldBe("b31424916e134fb9956a90f274b7b1e0237e587159aa42b99b9b2491e0ecd41e79699d75bbd24f52b84e0807d400f8732390bbed8c6d4d60ab21d7c094354a33d29dd30cc24c4f11b5a82d0de55c0b3f735a4dc1205045d0847525ca0c061d86ae2a34d5094d4a1eb55acbda20257643694550910ab1481f8e5d87f67bafe4b");
            result.Type.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupsAppService.DeleteAsync(Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"));

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"));

            result.ShouldBeNull();
        }
    }
}