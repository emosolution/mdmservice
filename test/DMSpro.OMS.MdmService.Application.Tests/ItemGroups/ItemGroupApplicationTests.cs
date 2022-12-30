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
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemGroupsAppService.GetListAsync(new GetItemGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("699c2938-8d71-4223-80a9-088e15868879")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("55b87b4f-c4fd-46ad-8a9f-cacef25649b4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemGroupsAppService.GetAsync(Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemGroupCreateDto
            {
                Code = "c37ba45b566245e892f637a9f4ba7267ba3f55d5f567487686",
                Name = "b036af640eee44a8a5d76a05622b2bc4fac9de11c8074ef1accbfc9c10a389",
                Description = "764ccf63867e408ba926b3390136af120adfd7ce97024b018816586dd1f9308eb550ffb6e6644854aaecc9f5d5e26ca7c4df3fbb85e941fe9b53580ed6ff5080416b69a401ac480fbc6dedc9bb6bd30abede40e0f6cb4c1ab9952f153ca462d04176454d617a40a084c62cfcb74a644a22a1de72d629467b8f01eba8a6da9f2",
                Type = default,
                Status = default
            };

            // Act
            var serviceResult = await _itemGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c37ba45b566245e892f637a9f4ba7267ba3f55d5f567487686");
            result.Name.ShouldBe("b036af640eee44a8a5d76a05622b2bc4fac9de11c8074ef1accbfc9c10a389");
            result.Description.ShouldBe("764ccf63867e408ba926b3390136af120adfd7ce97024b018816586dd1f9308eb550ffb6e6644854aaecc9f5d5e26ca7c4df3fbb85e941fe9b53580ed6ff5080416b69a401ac480fbc6dedc9bb6bd30abede40e0f6cb4c1ab9952f153ca462d04176454d617a40a084c62cfcb74a644a22a1de72d629467b8f01eba8a6da9f2");
            result.Type.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemGroupUpdateDto()
            {
                Code = "62aa4865e52d4ba2934dfff70ada078aba9b100b99cb42f998",
                Name = "1e68d6e3d616427089b707c3b687eb1e155e6eed60b94b41ad868f4222654f8bd265a30e0",
                Description = "e74408d9a0b043f299eddfadc467a71384e07ea783854b6fa6fe868c9643eaeb53efbc25212f4c1e8db271d1f7d29da0144a2774148e40c39b76a806aa2b1e010019d0ecf3384781a3282b430bcfb28f787f60e860034fd38a088554c0f69b99ff217b8c3f4a4dc99a924a95c0fc4ba36f8f0424984e4fc5852f1456cc5b5a6",
                Type = default,
                Status = default
            };

            // Act
            var serviceResult = await _itemGroupsAppService.UpdateAsync(Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"), input);

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("62aa4865e52d4ba2934dfff70ada078aba9b100b99cb42f998");
            result.Name.ShouldBe("1e68d6e3d616427089b707c3b687eb1e155e6eed60b94b41ad868f4222654f8bd265a30e0");
            result.Description.ShouldBe("e74408d9a0b043f299eddfadc467a71384e07ea783854b6fa6fe868c9643eaeb53efbc25212f4c1e8db271d1f7d29da0144a2774148e40c39b76a806aa2b1e010019d0ecf3384781a3282b430bcfb28f787f60e860034fd38a088554c0f69b99ff217b8c3f4a4dc99a924a95c0fc4ba36f8f0424984e4fc5852f1456cc5b5a6");
            result.Type.ShouldBe(default);
            result.Status.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemGroupsAppService.DeleteAsync(Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"));

            // Assert
            var result = await _itemGroupRepository.FindAsync(c => c.Id == Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"));

            result.ShouldBeNull();
        }
    }
}