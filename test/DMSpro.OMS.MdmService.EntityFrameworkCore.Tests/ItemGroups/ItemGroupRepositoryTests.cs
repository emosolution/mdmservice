using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupRepositoryTests()
        {
            _itemGroupRepository = GetRequiredService<IItemGroupRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupRepository.GetListAsync(
                    code: "548c63f3740c47dda15bbf4050c6b21223986c37545247f489",
                    name: "3f4af7093b2c4db",
                    description: "5c12998cdcab4f11a97c743ed5c9f3cd3ae98ae2f42a4635b28ee4788891a4fabf70aabb817a47c7a2ff8de9a6435e272ad0e0b08fc64c359363d9e2aacc6187d57aeb217dcc4405b8c6e7f723f9ca8f3699538c87544e4a996f0ec7ea3cabded95077832965492c8b0aecac6021ffa985b6125d74564d5290177b0629190d3",
                    type: default,
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("699c2938-8d71-4223-80a9-088e15868879"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemGroupRepository.GetCountAsync(
                    code: "29556f2d8875464e8c3f528c21ee7feddbde035e6a8d4d27b8",
                    name: "9946d987b7a24688885d48f7008148fbf1890e",
                    description: "9f6a4bf9deef4b109a017a33fcac1317c1999d29b1f2425e93ca30f7c471c885319d9b6f87994ba580ebe4b451f5072ecd3c2b78a6934b18834202f1dd729f86abce066c947946f680e8567cccbe5e352ed3e6ea8628474e9a44d78b53ed7e14366dbc73db23469f9bc24e184a21d4ddfd7068cbc86f428696318dfe66dea05",
                    type: default,
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}