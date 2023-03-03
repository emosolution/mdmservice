using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigRepositoryTests()
        {
            _numberingConfigRepository = GetRequiredService<INumberingConfigRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetListAsync(
                    prefix: "ae90690e5d1c49ecbebf",
                    suffix: "d191f000a5fa4e1e92f9"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b85fea39-2102-4295-9d6b-1bbb8dc7568c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetCountAsync(
                    prefix: "fe06d03b9e8b4e28ae66",
                    suffix: "dbe25d5eb7a84995b7ff"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}