using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISystemConfigsAppService _systemConfigsAppService;
        private readonly IRepository<SystemConfig, Guid> _systemConfigRepository;

        public SystemConfigsAppServiceTests()
        {
            _systemConfigsAppService = GetRequiredService<ISystemConfigsAppService>();
            _systemConfigRepository = GetRequiredService<IRepository<SystemConfig, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _systemConfigsAppService.GetListAsync(new GetSystemConfigsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("4df34775-c209-4a95-8cbc-4e5f3ac276f1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _systemConfigsAppService.GetAsync(Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SystemConfigCreateDto
            {
                Code = "7fa8019e54aa408b8a0d",
                Description = "de91e70c3026459bbb5af34ab6fec59a8dbee1c798c749eb930094ff2fe484aef328d232efb8437fb8facb6ca8e680f1112305ce38f04afcb5f57da541b256a0784fdb4b7f5346339708c7bb168aac0a8e98164821444717b1200b8a210ea4fca4122420a9cd4e00b771f532c3488aa4ffc97d754ea64c8a9451042100e79f12036233ea396d48ef9d6dae23ce9b7ae2a6ea853b4482469b818fb3cf57056892f15371491f6348ac9ece74bd12aa8299e41ee6f635274cb0a9c13bc0ea04dc1c611380e9dd494fae9338fd253901f669bc97959f25cb42df8d8c6cba8b09e0ba2a888628b08f4594b8066f7d16292f8c1bc2990e59544474bc28",
                Value = "67389b49ebb349f9894f6b966cc0b6fa2c63b8b6175c4035a1b144952607a7e8a613eacba9f3455db360b3f415866f4ccd827328f11c48319cd081309248c8cf776c153f97b04713a54085f42b4ecae6109c0678309844b0916824b2f865a45a7dc3577cabbd470a9751c9fe366b3b9d89306a7c335c4c8eb0c73de18674a65",
                DefaultValue = "9ffd6c2b1a4a4d5a8e37babd01eb78cb456e0eb153224f09892788d7226ec55b7dc51d10450b4ce598759e5fd185c5114baf2b2bbb304043911dca3e0393f46c148d1aae8cef4a0789023cdeac0cd0860bd5677ca2414b549e285f6058fbe27760875592671240e3818c151bb025a5efff7c6894fb284723abae9e061fee689",
                EditableByTenant = true,
                ControlType = default,
                DataSource = "2f0fab5469a94f8ea68c23b296"
            };

            // Act
            var serviceResult = await _systemConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7fa8019e54aa408b8a0d");
            result.Description.ShouldBe("de91e70c3026459bbb5af34ab6fec59a8dbee1c798c749eb930094ff2fe484aef328d232efb8437fb8facb6ca8e680f1112305ce38f04afcb5f57da541b256a0784fdb4b7f5346339708c7bb168aac0a8e98164821444717b1200b8a210ea4fca4122420a9cd4e00b771f532c3488aa4ffc97d754ea64c8a9451042100e79f12036233ea396d48ef9d6dae23ce9b7ae2a6ea853b4482469b818fb3cf57056892f15371491f6348ac9ece74bd12aa8299e41ee6f635274cb0a9c13bc0ea04dc1c611380e9dd494fae9338fd253901f669bc97959f25cb42df8d8c6cba8b09e0ba2a888628b08f4594b8066f7d16292f8c1bc2990e59544474bc28");
            result.Value.ShouldBe("67389b49ebb349f9894f6b966cc0b6fa2c63b8b6175c4035a1b144952607a7e8a613eacba9f3455db360b3f415866f4ccd827328f11c48319cd081309248c8cf776c153f97b04713a54085f42b4ecae6109c0678309844b0916824b2f865a45a7dc3577cabbd470a9751c9fe366b3b9d89306a7c335c4c8eb0c73de18674a65");
            result.DefaultValue.ShouldBe("9ffd6c2b1a4a4d5a8e37babd01eb78cb456e0eb153224f09892788d7226ec55b7dc51d10450b4ce598759e5fd185c5114baf2b2bbb304043911dca3e0393f46c148d1aae8cef4a0789023cdeac0cd0860bd5677ca2414b549e285f6058fbe27760875592671240e3818c151bb025a5efff7c6894fb284723abae9e061fee689");
            result.EditableByTenant.ShouldBe(true);
            result.ControlType.ShouldBe(default);
            result.DataSource.ShouldBe("2f0fab5469a94f8ea68c23b296");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SystemConfigUpdateDto()
            {
                Code = "6fbfecf8f2db43218596",
                Description = "665d64115bb44f4d96119679c691547ade666c338d5648cd895ae40f8699cadbb3100dca4673497ea8791c67feba019bd351a48dbfd541f1a19dcda0404836acab451ab906b64e0ca09fac89d4dfa33cbd316a288cc14e44b77a08bccd26db0fb6b341a6f60246a097595f3f84b01cfe4b585f06c21f460b80ce0cf38ad094eeff804693b440488cb4f872ddecace2eb9967318821c146868d0f47e0a032c183d2e4537d9ee74852a17fc2502fda730cf14694d36a544fcaac869bb9b4fa6fd2f2bf0377950849c78f29b1fb58b7dbf990cdc9d6c6a04aa89a07d3b56f07433070f5e5ee3f0745318570c7a869f4f9c9a1072e24606547f9a34c",
                Value = "087d073c341b4910b81af6b62481cefebc37ada62e4a40ca87d14b1e9332844033af97274b594678a4659b175ef447959e852611c1034ed1b37724fce4a199aba63ac7d7715f4c0496affbcc6c5cb05af7b6cb4205bd4b1bb506bb14aaf43595d928b1b3c18142a48ea86b9967c095af7d24f13966bc4634868e82326aa3bed",
                DefaultValue = "97f2d65259c344b6a0930045b2fc8980ef2e3bbfb1cc4438960af8c2fabf3ba53bd7c22a97af4a3ca3f4bbe4c8fe0a294fc5dc85dc274c6c8b11c75a96d71e873d15951582d14b488706e21fc6412e62d526943219994a6ba1cd1cbdfc39c92d188baddb381e424a8171a1796fd66f303609c712718845f2b13e6728823a6af",
                EditableByTenant = true,
                ControlType = default,
                DataSource = "679d8e51bf744552a25b646430524683ddb5be77a4994aa48c9a0f3d6faa674cf"
            };

            // Act
            var serviceResult = await _systemConfigsAppService.UpdateAsync(Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"), input);

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("6fbfecf8f2db43218596");
            result.Description.ShouldBe("665d64115bb44f4d96119679c691547ade666c338d5648cd895ae40f8699cadbb3100dca4673497ea8791c67feba019bd351a48dbfd541f1a19dcda0404836acab451ab906b64e0ca09fac89d4dfa33cbd316a288cc14e44b77a08bccd26db0fb6b341a6f60246a097595f3f84b01cfe4b585f06c21f460b80ce0cf38ad094eeff804693b440488cb4f872ddecace2eb9967318821c146868d0f47e0a032c183d2e4537d9ee74852a17fc2502fda730cf14694d36a544fcaac869bb9b4fa6fd2f2bf0377950849c78f29b1fb58b7dbf990cdc9d6c6a04aa89a07d3b56f07433070f5e5ee3f0745318570c7a869f4f9c9a1072e24606547f9a34c");
            result.Value.ShouldBe("087d073c341b4910b81af6b62481cefebc37ada62e4a40ca87d14b1e9332844033af97274b594678a4659b175ef447959e852611c1034ed1b37724fce4a199aba63ac7d7715f4c0496affbcc6c5cb05af7b6cb4205bd4b1bb506bb14aaf43595d928b1b3c18142a48ea86b9967c095af7d24f13966bc4634868e82326aa3bed");
            result.DefaultValue.ShouldBe("97f2d65259c344b6a0930045b2fc8980ef2e3bbfb1cc4438960af8c2fabf3ba53bd7c22a97af4a3ca3f4bbe4c8fe0a294fc5dc85dc274c6c8b11c75a96d71e873d15951582d14b488706e21fc6412e62d526943219994a6ba1cd1cbdfc39c92d188baddb381e424a8171a1796fd66f303609c712718845f2b13e6728823a6af");
            result.EditableByTenant.ShouldBe(true);
            result.ControlType.ShouldBe(default);
            result.DataSource.ShouldBe("679d8e51bf744552a25b646430524683ddb5be77a4994aa48c9a0f3d6faa674cf");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _systemConfigsAppService.DeleteAsync(Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"));

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == Guid.Parse("ff279c6a-2798-4c3f-9686-75427f1fe625"));

            result.ShouldBeNull();
        }
    }
}