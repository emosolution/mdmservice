using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IUOMGroupsAppService _uOMGroupsAppService;
        private readonly IRepository<UOMGroup, Guid> _uOMGroupRepository;

        public UOMGroupsAppServiceTests()
        {
            _uOMGroupsAppService = GetRequiredService<IUOMGroupsAppService>();
            _uOMGroupRepository = GetRequiredService<IRepository<UOMGroup, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _uOMGroupsAppService.GetListAsync(new GetUOMGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("16091d7b-9646-487e-be19-36b00a1f6d0b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _uOMGroupsAppService.GetAsync(Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UOMGroupCreateDto
            {
                Code = "7d7420f00f2744e78fb6",
                Name = "27dc932b51fe441ea1d035c98fe557b9005bc2098c3b46c59dd783f5b0e4c20b61c0cf15122e44c283bbda15465ecc487b452b5f20454d84875c5c9bb4db18e8265038500a774f968c7f7e66c347e7c5780c26353f314c3b9e841baa541a9845fdf23f2b"
            };

            // Act
            var serviceResult = await _uOMGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _uOMGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7d7420f00f2744e78fb6");
            result.Name.ShouldBe("27dc932b51fe441ea1d035c98fe557b9005bc2098c3b46c59dd783f5b0e4c20b61c0cf15122e44c283bbda15465ecc487b452b5f20454d84875c5c9bb4db18e8265038500a774f968c7f7e66c347e7c5780c26353f314c3b9e841baa541a9845fdf23f2b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UOMGroupUpdateDto()
            {
                Code = "dcd281da41944f7da136",
                Name = "0d4794162dd34a7bb0f9064e16493ad136cd382e6894443eb5dc5cb2acd164d9cd7444a9d56f40dfbd0ffa663bf78f7fdc0aa9dc05454cd3a09d9a9ff11c713bb68a7440057c451c8385e522a3241731366722eec5e94b23a22fbd7e2b7a9e72888d23d4"
            };

            // Act
            var serviceResult = await _uOMGroupsAppService.UpdateAsync(Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"), input);

            // Assert
            var result = await _uOMGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("dcd281da41944f7da136");
            result.Name.ShouldBe("0d4794162dd34a7bb0f9064e16493ad136cd382e6894443eb5dc5cb2acd164d9cd7444a9d56f40dfbd0ffa663bf78f7fdc0aa9dc05454cd3a09d9a9ff11c713bb68a7440057c451c8385e522a3241731366722eec5e94b23a22fbd7e2b7a9e72888d23d4");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _uOMGroupsAppService.DeleteAsync(Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"));

            // Assert
            var result = await _uOMGroupRepository.FindAsync(c => c.Id == Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"));

            result.ShouldBeNull();
        }
    }
}