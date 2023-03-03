using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IWorkingPositionRepository _workingPositionRepository;

        public WorkingPositionRepositoryTests()
        {
            _workingPositionRepository = GetRequiredService<IWorkingPositionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _workingPositionRepository.GetListAsync(
                    code: "c1d05beb3047403ab3be",
                    name: "be6ca50fce9544ed98c612602217a8b724ff56b32c2c484589defb5faada16a2767546821eb2455c9ab957af2974989f6a273595eaaf4e2196b9d0474489011446e64d98506f4aa08209f59784cabbaf01eebb5815d9439898acc79d70f11e5bf90d1939b2804bd0b44da08c0588ecf4eb91de0e9b824dc9b7bc3506180c24c",
                    description: "5e0081e8f7a6499290b5781f34e92299f30cdc0c53b74b4b9b1fefa637bf3e95efc5d8c4d70642629e6e42093cb26a53258283926bf640fe86a07a17f5c9e757ba3b67c9b05e4e68b4005c3b6a1e493fdf633ae9fce84ca8958c0e9b07cba042e22e3468e77e4f3f8aa4551dcb6c5daadb2f773e329b4cf08c1db32641b8fd67d8d0bebcf0e34da680cf46f2d2489ea2df9cf9410ef042bb82776ca8042bd9c0a8f03a8d73c749088285bc6ea3a42d72bff58b633f0d435186cc78d1f1166442727ea227882d4a77aa6e3af8ed94d2a1d1722b7750fc45ec8058fc4a61f109fc1efb21089fd84816b51c8dc15344a2e6d4d1a4d960704aeea864",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _workingPositionRepository.GetCountAsync(
                    code: "a4666d97a639448dac14",
                    name: "6a6a9c87f5394cd8b29b94b32075f25622285ea599954f4288dbe3622d07e0b1f8a194f7278b4f6aaab745d06c090372a0e11bd935a2478887f4f3fab41829b53278ab9feada4741b40514f001bebbd1d668215d93d84972a6ca37956e9652ac83153aaaab4a41c1a7a99e45004f22a5f6b7fb132fff4603ac4f290f0869ce1",
                    description: "8fdd9b514fa041d5855e3929211b2e1ae6b5b2245a4f4fc4885395e3d3b7af07f76f483637f4424cb4466ee2ea72add5d0f3f62e1d9c403592c9be2905e056c579f5d2aad04a49e498930e7a0e65e8ae15ee727e7fe04682a098cdce5152b718cd34e8eea8f449938b3300f82804e5b770c0823da3fb470181ac5fdfb5152a079009ed77fb99466faa7d848b37a30a0fd52fa86b19af4b8c8cee5eb6b97ee8d8fb22e8bb80704cd7ab5e74744d34858a700eb4e79ee34308a9ae8dd86fbd65c8ba50a605175642bab1a03c56fd4f48cf70fb5478ca7541c1b8539ca704323be7a92957bfb3f54b79a3d676aa69ff724c33354dc094174f62bfcc",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}