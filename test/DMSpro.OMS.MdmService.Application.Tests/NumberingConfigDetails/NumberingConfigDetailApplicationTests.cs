using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly INumberingConfigDetailsAppService _numberingConfigDetailsAppService;
        private readonly IRepository<NumberingConfigDetail, Guid> _numberingConfigDetailRepository;

        public NumberingConfigDetailsAppServiceTests()
        {
            _numberingConfigDetailsAppService = GetRequiredService<INumberingConfigDetailsAppService>();
            _numberingConfigDetailRepository = GetRequiredService<IRepository<NumberingConfigDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _numberingConfigDetailsAppService.GetListAsync(new GetNumberingConfigDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.NumberingConfigDetail.Id == Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfigDetail.Id == Guid.Parse("24134bf2-0daa-4798-b3e4-93cd61ecd7a1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigDetailsAppService.GetAsync(Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigDetailCreateDto
            {
                Description = "5bed5081a86949c58d12693a47db7db9bc55a0b082d84fa495823f0a06eb8da90d5712ff0203489ba4218add1f70704ae4edb416c02546a1a6614028d48c5dd16ca7bcafdfea45f8a5c55da9210bc8d77aed7456a25f4dc59eda1eb79ca1db7e41dec6940471436da2830491ad63ecdd1d13b949e879436c8d45fd00a0888e0",
                Prefix = "a9d4a7c454a24a26aab9",
                PaddingZeroNumber = 348970507,
                Suffix = "45b492abc8684adebe68",
                Active = true,
                CurrentNumber = 1057090696,
                NumberingConfigId = Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            };

            // Act
            var serviceResult = await _numberingConfigDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("5bed5081a86949c58d12693a47db7db9bc55a0b082d84fa495823f0a06eb8da90d5712ff0203489ba4218add1f70704ae4edb416c02546a1a6614028d48c5dd16ca7bcafdfea45f8a5c55da9210bc8d77aed7456a25f4dc59eda1eb79ca1db7e41dec6940471436da2830491ad63ecdd1d13b949e879436c8d45fd00a0888e0");
            result.Prefix.ShouldBe("a9d4a7c454a24a26aab9");
            result.PaddingZeroNumber.ShouldBe(348970507);
            result.Suffix.ShouldBe("45b492abc8684adebe68");
            result.Active.ShouldBe(true);
            result.CurrentNumber.ShouldBe(1057090696);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigDetailUpdateDto()
            {
                Description = "30870a23b32542f4b5fa049408e761460b0fa48a8b53479cb614cee71c71f7b76a3f00d06534404d8ca548b50f437697baf124883ef44801ae7d4f83819e035afaf80154b0634448948b61a709a0ac3a94ff53fd55db427cbe5b3e081e10f417dc5dc9a3b70144afb349eca4314f64a5d4ab5cb38b9f4839ab69206e1d61757",
                Prefix = "16eb4c35aea54f9faa15",
                PaddingZeroNumber = 816869303,
                Suffix = "cbfb4f9c6da149309b82",
                Active = true,
                CurrentNumber = 127301280,
                NumberingConfigId = Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            };

            // Act
            var serviceResult = await _numberingConfigDetailsAppService.UpdateAsync(Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"), input);

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("30870a23b32542f4b5fa049408e761460b0fa48a8b53479cb614cee71c71f7b76a3f00d06534404d8ca548b50f437697baf124883ef44801ae7d4f83819e035afaf80154b0634448948b61a709a0ac3a94ff53fd55db427cbe5b3e081e10f417dc5dc9a3b70144afb349eca4314f64a5d4ab5cb38b9f4839ab69206e1d61757");
            result.Prefix.ShouldBe("16eb4c35aea54f9faa15");
            result.PaddingZeroNumber.ShouldBe(816869303);
            result.Suffix.ShouldBe("cbfb4f9c6da149309b82");
            result.Active.ShouldBe(true);
            result.CurrentNumber.ShouldBe(127301280);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigDetailsAppService.DeleteAsync(Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"));

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"));

            result.ShouldBeNull();
        }
    }
}