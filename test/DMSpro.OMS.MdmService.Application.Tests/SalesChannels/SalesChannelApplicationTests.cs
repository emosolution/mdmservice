using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesChannelsAppService _salesChannelsAppService;
        private readonly IRepository<SalesChannel, Guid> _salesChannelRepository;

        public SalesChannelsAppServiceTests()
        {
            _salesChannelsAppService = GetRequiredService<ISalesChannelsAppService>();
            _salesChannelRepository = GetRequiredService<IRepository<SalesChannel, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesChannelsAppService.GetListAsync(new GetSalesChannelsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("0e013b93-f1f3-4257-937e-84f3c03a4748")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesChannelsAppService.GetAsync(Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesChannelCreateDto
            {
                Code = "c6abe8a2769648688080",
                Name = "171bbbd5bad24d13af6e4f6dde503ece5c0e715c3c6a44d7aa3e56a2fedd9d06c83c2a621650422e81379ca10d4490c37da671e5a0f1400a8eaabe016f1e7b50479a9570565f408f822012a09464bd67b6381a754ff8453e85df104715beebab97ee3075",
                Description = "f5425ae31afe4a1598a2b5ebd04de5ce20ed7cea7a0349c191d06130267e45c3ed64a3ffebbc40f9ae71fe1b96141cd4438573ae0b37402c85fd4894ac33925b14a9eef232a24aefa74c7e8534a988ee7464f25033844c4e836118241cc73c438dfbd10c572142e4b089979312a08724496326ae8f984fb49590d5878c30774dd2a462a89ca54c37ba2d34694c446ffc7de1581a624d403891e74654ca3b7f0bec8041ed7b5e4faa864f09c39d4b50ff1c2cc822da764c28805ee15293313dc1590b3831b3af4db1a36c071d8f47f1ab51d9c75c156e4a39b2e90f764451a4ee150020744cbf4a7f97468e47d3d0159dc569ee707b3747829284",
                Active = true
            };

            // Act
            var serviceResult = await _salesChannelsAppService.CreateAsync(input);

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c6abe8a2769648688080");
            result.Name.ShouldBe("171bbbd5bad24d13af6e4f6dde503ece5c0e715c3c6a44d7aa3e56a2fedd9d06c83c2a621650422e81379ca10d4490c37da671e5a0f1400a8eaabe016f1e7b50479a9570565f408f822012a09464bd67b6381a754ff8453e85df104715beebab97ee3075");
            result.Description.ShouldBe("f5425ae31afe4a1598a2b5ebd04de5ce20ed7cea7a0349c191d06130267e45c3ed64a3ffebbc40f9ae71fe1b96141cd4438573ae0b37402c85fd4894ac33925b14a9eef232a24aefa74c7e8534a988ee7464f25033844c4e836118241cc73c438dfbd10c572142e4b089979312a08724496326ae8f984fb49590d5878c30774dd2a462a89ca54c37ba2d34694c446ffc7de1581a624d403891e74654ca3b7f0bec8041ed7b5e4faa864f09c39d4b50ff1c2cc822da764c28805ee15293313dc1590b3831b3af4db1a36c071d8f47f1ab51d9c75c156e4a39b2e90f764451a4ee150020744cbf4a7f97468e47d3d0159dc569ee707b3747829284");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesChannelUpdateDto()
            {
                Code = "63449c2311354d878490",
                Name = "3399d8e2abb442f6a14b8d9fc09ecfc3a1ff80c3941743798fd7dccf907aafbc757c81046f634e889ac6c1ac9f09782a968da94a3b1342c39297b25295033a95fc6ade61181044e2938070b7365ee070e411aedba49c4445bccb5f8e9cd880bb19805788",
                Description = "b0e54b7c66f240b388be753c943d9dd18634fd0b6f144b158b4afc77206ddfc83aa2265cbe9e4f07bd0ce8f864f1c6dc5cb85afd48d14578a0d80b0bac5f24d37b87a262aaf747d8a1a591e75f120af2c803a1a74d2547969f519329e7128c6d61a9461c0e584c7ea9a8138f698ce55df910929a87334ab7a8dd057e59828dd3fef465c024e247c18027a3a66cdca1ff33c0308b6e3e48ec9685f74db3213952357a1aa29cd2475284b2a0bbb7b97884d4b4d34bc45c4cda8539740f48578f2576bfa3285e714c04a6150a57823053f4e30fa5c357da4061829e6a9cfac9c6f236106f4753044a08a7883caf544140ec445b24922ea748bf97bb",
                Active = true
            };

            // Act
            var serviceResult = await _salesChannelsAppService.UpdateAsync(Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"), input);

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("63449c2311354d878490");
            result.Name.ShouldBe("3399d8e2abb442f6a14b8d9fc09ecfc3a1ff80c3941743798fd7dccf907aafbc757c81046f634e889ac6c1ac9f09782a968da94a3b1342c39297b25295033a95fc6ade61181044e2938070b7365ee070e411aedba49c4445bccb5f8e9cd880bb19805788");
            result.Description.ShouldBe("b0e54b7c66f240b388be753c943d9dd18634fd0b6f144b158b4afc77206ddfc83aa2265cbe9e4f07bd0ce8f864f1c6dc5cb85afd48d14578a0d80b0bac5f24d37b87a262aaf747d8a1a591e75f120af2c803a1a74d2547969f519329e7128c6d61a9461c0e584c7ea9a8138f698ce55df910929a87334ab7a8dd057e59828dd3fef465c024e247c18027a3a66cdca1ff33c0308b6e3e48ec9685f74db3213952357a1aa29cd2475284b2a0bbb7b97884d4b4d34bc45c4cda8539740f48578f2576bfa3285e714c04a6150a57823053f4e30fa5c357da4061829e6a9cfac9c6f236106f4753044a08a7883caf544140ec445b24922ea748bf97bb");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesChannelsAppService.DeleteAsync(Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"));

            // Assert
            var result = await _salesChannelRepository.FindAsync(c => c.Id == Guid.Parse("c902f45b-606e-4505-8504-bd23485c7bc0"));

            result.ShouldBeNull();
        }
    }
}