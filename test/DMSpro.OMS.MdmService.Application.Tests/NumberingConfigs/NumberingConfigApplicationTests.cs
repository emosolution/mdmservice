using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly INumberingConfigsAppService _numberingConfigsAppService;
        private readonly IRepository<NumberingConfig, Guid> _numberingConfigRepository;

        public NumberingConfigsAppServiceTests()
        {
            _numberingConfigsAppService = GetRequiredService<INumberingConfigsAppService>();
            _numberingConfigRepository = GetRequiredService<IRepository<NumberingConfig, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetListAsync(new GetNumberingConfigsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfig.Id == Guid.Parse("8130f1cd-bb3e-4967-8c16-ce1dbc47c48b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigsAppService.GetAsync(Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigCreateDto
            {
                StartNumber = 1984619074,
                Prefix = "05c01747ed094750b310",
                Suffix = "81788fdccff54246996c",
                Length = 241419465,
                Active = true,
                Description = "77b60566a2ac42a3ab60098bcd725a47715d905bb1ae46b3b4afa8bb72e780b0a663461e2513419ebcbbe6850cbb2d8cee419799d2ff4fad857941ea448cfa04db1a18c588224a69b61a186d241104553622cc91d6e74e1089a1c10e8de23f88c0fef76f554642d9bbdbebcd040620d9e31b363b48044bec9d303af0cc52bbf",
                IsDefault = true
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1984619074);
            result.Prefix.ShouldBe("05c01747ed094750b310");
            result.Suffix.ShouldBe("81788fdccff54246996c");
            result.Length.ShouldBe(241419465);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("77b60566a2ac42a3ab60098bcd725a47715d905bb1ae46b3b4afa8bb72e780b0a663461e2513419ebcbbe6850cbb2d8cee419799d2ff4fad857941ea448cfa04db1a18c588224a69b61a186d241104553622cc91d6e74e1089a1c10e8de23f88c0fef76f554642d9bbdbebcd040620d9e31b363b48044bec9d303af0cc52bbf");
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigUpdateDto()
            {
                StartNumber = 1976847671,
                Prefix = "41afa850d8904315aa40",
                Suffix = "5fc5710113584fcd82b7",
                Length = 148881755,
                Active = true,
                Description = "a2e127c5f29c473e9718fc1e0275d42ca08f707f76c34b5dafddcf903d8ee07dfa5c46ae72894898979f7f5aa8ccfca32cac707145aa4115bebe21664cb1ab1603fc5f4f189c4004bb5d77a1ff4fb58bf569309a1baf4fe5b341824772348fc3e81974c896314644a91ca1d7a5741d8f1cb8117316b64d14853f7f67c754f24",
                IsDefault = true
            };

            // Act
            var serviceResult = await _numberingConfigsAppService.UpdateAsync(Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"), input);

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartNumber.ShouldBe(1976847671);
            result.Prefix.ShouldBe("41afa850d8904315aa40");
            result.Suffix.ShouldBe("5fc5710113584fcd82b7");
            result.Length.ShouldBe(148881755);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("a2e127c5f29c473e9718fc1e0275d42ca08f707f76c34b5dafddcf903d8ee07dfa5c46ae72894898979f7f5aa8ccfca32cac707145aa4115bebe21664cb1ab1603fc5f4f189c4004bb5d77a1ff4fb58bf569309a1baf4fe5b341824772348fc3e81974c896314644a91ca1d7a5741d8f1cb8117316b64d14853f7f67c754f24");
            result.IsDefault.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigsAppService.DeleteAsync(Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"));

            // Assert
            var result = await _numberingConfigRepository.FindAsync(c => c.Id == Guid.Parse("6c1c601c-4aa2-4711-a528-c28c7f3f89b9"));

            result.ShouldBeNull();
        }
    }
}