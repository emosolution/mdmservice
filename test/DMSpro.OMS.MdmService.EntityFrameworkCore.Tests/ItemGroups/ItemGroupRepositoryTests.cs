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
                    code: "746d99bac73a4cd6b81003c9a1bd3af51cba731473a745e881",
                    name: "efbd496f87554979aff993fb19fa143f9ab9e94c2b014ab6944e7c1b0c59faf5b82fb09f26764f",
                    description: "49a9f322f6a94fccbc865d558ba6e723ec4ccac433514a8588ffaf7678c471c703dac16ea729486999fc9e50915b91c0b00611d471604282a5ca20a09e1637aef5591df420bb46668ee8a4b53d348ce98a981028897e424bb85d1b2d895fc7ff720583a06d694182b6d72d166bdf6bca83ef1cc67a6a441cac00d8134c13a1d",
                    type: default,
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f"));
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
                    code: "39a5ad3d3dab48ac869d4b76d45ce8281e45992b5ac14669ab",
                    name: "0302befb22584420a54e3158031c10256b2e800e48b042c1ab6cda577d4dccb724c38be74",
                    description: "3969f07e88594c91be085b32d5faa0eff5455ef35636415088c42ba33358b42d2e5acb44c7154ed89934d1a90b67b1e4b73ac1773a67437980179da21fb9885e381c6b19e9ff4f17a8b2f7bad051f5f293ef922e01404e3d8d364d2684fbd2212ef7b3a016e64fb6ad3b297421664aec0fddee54350148bda126b870193470f",
                    type: default,
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}