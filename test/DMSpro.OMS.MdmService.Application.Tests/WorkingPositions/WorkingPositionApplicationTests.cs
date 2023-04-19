using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IWorkingPositionsAppService _workingPositionsAppService;
        private readonly IRepository<WorkingPosition, Guid> _workingPositionRepository;

        public WorkingPositionsAppServiceTests()
        {
            _workingPositionsAppService = GetRequiredService<IWorkingPositionsAppService>();
            _workingPositionRepository = GetRequiredService<IRepository<WorkingPosition, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _workingPositionsAppService.GetAsync(Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WorkingPositionCreateDto
            {
                Code = "86e5fb58a68949d2ab2d",
                Name = "f6358cd392af4dc79c496b96b67567cdca59595f48644801be1571a5ea123ccdca7c585b98014166a0e9f51c4acc662d6816531864ea4bc5ab874a2584c393b9338485d448f64e33b463b0fc1039ef1e8ad55505f20d4baa91f6ecf52c2afba646052671d8f34fa1a201d78ce3e5a2e494037d4513ed40bcb40240e99cdee5f",
                Description = "46de9835b99c433eb009c9bfcece8e4c064b9318324d45b2aaac09304279d66c3f759638f76040fb88d7c6dae105b96b1a6d555324f840cda42ffbdadd886bda55b9f0d6349842f6b6fe34bcb1a78cabbb50fd69789b4c2f9511609267b05371b2101a937dc945e38062749e960f1380b3b441c9f82f4e92b48f1f7281d2ba95071715704c944e119f5ab30bb324131f6dcff386effa469584f5889ddf4a638889d967f340b144d89431b4ed3a70542ddec14adf39674b87ba3dcf0dc4f6a2dc6c0aa6a683bf467d84edbfbcd28d17d67a88fb7585464fce91ee8c9a12e669d56e47b98de4934a3f9fdbac13f01c57b3a5e857a04213422ba7c1",
                Active = true
            };

            // Act
            var serviceResult = await _workingPositionsAppService.CreateAsync(input);

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("86e5fb58a68949d2ab2d");
            result.Name.ShouldBe("f6358cd392af4dc79c496b96b67567cdca59595f48644801be1571a5ea123ccdca7c585b98014166a0e9f51c4acc662d6816531864ea4bc5ab874a2584c393b9338485d448f64e33b463b0fc1039ef1e8ad55505f20d4baa91f6ecf52c2afba646052671d8f34fa1a201d78ce3e5a2e494037d4513ed40bcb40240e99cdee5f");
            result.Description.ShouldBe("46de9835b99c433eb009c9bfcece8e4c064b9318324d45b2aaac09304279d66c3f759638f76040fb88d7c6dae105b96b1a6d555324f840cda42ffbdadd886bda55b9f0d6349842f6b6fe34bcb1a78cabbb50fd69789b4c2f9511609267b05371b2101a937dc945e38062749e960f1380b3b441c9f82f4e92b48f1f7281d2ba95071715704c944e119f5ab30bb324131f6dcff386effa469584f5889ddf4a638889d967f340b144d89431b4ed3a70542ddec14adf39674b87ba3dcf0dc4f6a2dc6c0aa6a683bf467d84edbfbcd28d17d67a88fb7585464fce91ee8c9a12e669d56e47b98de4934a3f9fdbac13f01c57b3a5e857a04213422ba7c1");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WorkingPositionUpdateDto()
            {
                Name = "290111e992c24b22b34823cdc966f4e3981919036d0947ad815b2968730e244e1abfcb6b510f47c1890ee678a3c45d4eb1e9808d6c354019a03f4c606a303fad936cd7fc0af84ce3a5b3207b8feb84a1f37293ba8d1f441995c672cf268f5b301848eab8471342fe8fa915bb3a6b324174b41895a18e4147860627f7e8faa5a",
                Description = "46e0f7e6d3b542aa904a33f32d4ce1a2f6d18f38cfc947e0b11dc3edb0dc943399dec2ad001a4e4f9bc792e91fe116f3bbee4657e67643619615661a908107eded3838e0660544bab59f0be438819c92cbc81192bf3c4508babd7c810fb25954c36c2cc9c753451bbdfa375b0d379953a1e6c33cb367465d80a1afbe675351db69857edbc66745269951405dadaa9fc119dc1acc57d74118a4cdb067ae835eb9b80107580bb64f5e8a93c62fc091294a46d47e08471247bfa8f8b90fbc1388a4feaf85937b984197a4aa94576c4687ff14b6ee939dac48e188b9bfead99bdb090639398fa14a4dae8bea33d4967596402ad26c90780c473a909a",
                Active = true
            };

            // Act
            var serviceResult = await _workingPositionsAppService.UpdateAsync(Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"), input);

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("290111e992c24b22b34823cdc966f4e3981919036d0947ad815b2968730e244e1abfcb6b510f47c1890ee678a3c45d4eb1e9808d6c354019a03f4c606a303fad936cd7fc0af84ce3a5b3207b8feb84a1f37293ba8d1f441995c672cf268f5b301848eab8471342fe8fa915bb3a6b324174b41895a18e4147860627f7e8faa5a");
            result.Description.ShouldBe("46e0f7e6d3b542aa904a33f32d4ce1a2f6d18f38cfc947e0b11dc3edb0dc943399dec2ad001a4e4f9bc792e91fe116f3bbee4657e67643619615661a908107eded3838e0660544bab59f0be438819c92cbc81192bf3c4508babd7c810fb25954c36c2cc9c753451bbdfa375b0d379953a1e6c33cb367465d80a1afbe675351db69857edbc66745269951405dadaa9fc119dc1acc57d74118a4cdb067ae835eb9b80107580bb64f5e8a93c62fc091294a46d47e08471247bfa8f8b90fbc1388a4feaf85937b984197a4aa94576c4687ff14b6ee939dac48e188b9bfead99bdb090639398fa14a4dae8bea33d4967596402ad26c90780c473a909a");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _workingPositionsAppService.DeleteAsync(Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"));

            // Assert
            var result = await _workingPositionRepository.FindAsync(c => c.Id == Guid.Parse("e08027cb-400d-48f2-b73c-376d57f32c3b"));

            result.ShouldBeNull();
        }
    }
}