using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeadersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IMCPHeadersAppService _mCPHeadersAppService;
        private readonly IRepository<MCPHeader, Guid> _mCPHeaderRepository;

        public MCPHeadersAppServiceTests()
        {
            _mCPHeadersAppService = GetRequiredService<IMCPHeadersAppService>();
            _mCPHeaderRepository = GetRequiredService<IRepository<MCPHeader, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _mCPHeadersAppService.GetListAsync(new GetMCPHeadersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a")).ShouldBe(true);
            result.Items.Any(x => x.MCPHeader.Id == Guid.Parse("af2c85a3-9be9-408f-99d2-90789e7bee0a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _mCPHeadersAppService.GetAsync(Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MCPHeaderCreateDto
            {
                Code = "2d901c628b614f47a37a",
                Name = "e0278420e49c4d4195b02a7b1f425d385b547f187cda46a78e8be09452a2e3b1c535b5f16c12431f89eb845d6190499f5e782ebbec4841adaba310218e88f51d38f17f9c76c944c185ca73a234e8a9f313427ee32877414ebdabc152ff5520a6cac03897823d4df6a5303cc62adbe0c7d492905a3876457c8ac33bf374ff948",
                EffectiveDate = new DateTime(2007, 8, 25),
                EndDate = new DateTime(2008, 1, 8),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _mCPHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("2d901c628b614f47a37a");
            result.Name.ShouldBe("e0278420e49c4d4195b02a7b1f425d385b547f187cda46a78e8be09452a2e3b1c535b5f16c12431f89eb845d6190499f5e782ebbec4841adaba310218e88f51d38f17f9c76c944c185ca73a234e8a9f313427ee32877414ebdabc152ff5520a6cac03897823d4df6a5303cc62adbe0c7d492905a3876457c8ac33bf374ff948");
            result.EffectiveDate.ShouldBe(new DateTime(2007, 8, 25));
            result.EndDate.ShouldBe(new DateTime(2008, 1, 8));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MCPHeaderUpdateDto()
            {
                Code = "ea14787bdd944d448b0c",
                Name = "ecbd1881189d4e958b1cbe5971867bcc9f8802e1c3f0442e8696eeb9192c06f934d8eb5abad94ec3a925fea57cbc7b09dcdca4b6238046a4a499f4b401e0eec9a814e5960553411db28846b5fc7dab530725d98bcaa7470c87934167f6f6dd86adaa7ad063ce454099f239f9379f7cd30c9a66ca57bc45ebb6b6b850920e639",
                EffectiveDate = new DateTime(2007, 6, 15),
                EndDate = new DateTime(2018, 1, 13),
                RouteId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _mCPHeadersAppService.UpdateAsync(Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"), input);

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ea14787bdd944d448b0c");
            result.Name.ShouldBe("ecbd1881189d4e958b1cbe5971867bcc9f8802e1c3f0442e8696eeb9192c06f934d8eb5abad94ec3a925fea57cbc7b09dcdca4b6238046a4a499f4b401e0eec9a814e5960553411db28846b5fc7dab530725d98bcaa7470c87934167f6f6dd86adaa7ad063ce454099f239f9379f7cd30c9a66ca57bc45ebb6b6b850920e639");
            result.EffectiveDate.ShouldBe(new DateTime(2007, 6, 15));
            result.EndDate.ShouldBe(new DateTime(2018, 1, 13));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _mCPHeadersAppService.DeleteAsync(Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"));

            // Assert
            var result = await _mCPHeaderRepository.FindAsync(c => c.Id == Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"));

            result.ShouldBeNull();
        }
    }
}