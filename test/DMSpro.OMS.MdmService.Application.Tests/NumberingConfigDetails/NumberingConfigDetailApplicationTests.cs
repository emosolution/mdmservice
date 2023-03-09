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
            result.Items.Any(x => x.NumberingConfigDetail.Id == Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d")).ShouldBe(true);
            result.Items.Any(x => x.NumberingConfigDetail.Id == Guid.Parse("3d05a31b-7f2b-45cc-9107-9ad156da00da")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _numberingConfigDetailsAppService.GetAsync(Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new NumberingConfigDetailCreateDto
            {
                Active = true,
                Description = "c8192dd5858a4e61b5034a608bc2e06f32f1747871184aa58eb57effbd9db52c578b4f621bb54632864f1bd2dd4933d566725739adbb4cee8cc1f6d76f51767e8630f78f4bdb4a7a92ab0c060e67beff521375586c254a66a4557d2efbdfbe1df5b0b8639c334c56a08253458b86582c79d6b4f5b73347039282e875a251276",
                NumberingConfigId = Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            };

            // Act
            var serviceResult = await _numberingConfigDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("c8192dd5858a4e61b5034a608bc2e06f32f1747871184aa58eb57effbd9db52c578b4f621bb54632864f1bd2dd4933d566725739adbb4cee8cc1f6d76f51767e8630f78f4bdb4a7a92ab0c060e67beff521375586c254a66a4557d2efbdfbe1df5b0b8639c334c56a08253458b86582c79d6b4f5b73347039282e875a251276");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new NumberingConfigDetailUpdateDto()
            {
                Active = true,
                Description = "43ff167ae4da4bc089c59bcbbf9cfb66173f9517cbb54104b922a048178cbc14f163636b256245049464b035c8cece38fa09f66430b24853bbd8bd884bce17ec9d91543981fb4ad3bc5cf4bd6178f70aca4a3059f9854588a41a12b1b3655cf39535cbd05c6948c1b045345aac346ff05680036a419948869ffcc0f767dd388",
                NumberingConfigId = Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            };

            // Act
            var serviceResult = await _numberingConfigDetailsAppService.UpdateAsync(Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"), input);

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Active.ShouldBe(true);
            result.Description.ShouldBe("43ff167ae4da4bc089c59bcbbf9cfb66173f9517cbb54104b922a048178cbc14f163636b256245049464b035c8cece38fa09f66430b24853bbd8bd884bce17ec9d91543981fb4ad3bc5cf4bd6178f70aca4a3059f9854588a41a12b1b3655cf39535cbd05c6948c1b045345aac346ff05680036a419948869ffcc0f767dd388");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _numberingConfigDetailsAppService.DeleteAsync(Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"));

            // Assert
            var result = await _numberingConfigDetailRepository.FindAsync(c => c.Id == Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"));

            result.ShouldBeNull();
        }
    }
}