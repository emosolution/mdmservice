using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IMCPHeaderRepository _mCPHeaderRepository;

        public MCPHeaderRepositoryTests()
        {
            _mCPHeaderRepository = GetRequiredService<IMCPHeaderRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _mCPHeaderRepository.GetListAsync(
                    code: "2df2bee569f844bc92ce",
                    name: "a58b4c9ff1134803824ed6baccb0746578f30fc8dded4ed1830ce6f0755ed8d1c316ccff74474528832dfce6d13031fa5aff87cbacd9444cb8d537011ba84e90a0389d8d8dda42ed99d7b59f0c0874ce7aff65c70f03412482513957d5c05e78c613d2a709c8463db788d670e56e0f20a6d3daaad11e4a3583ff61e319cd840"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a4ad8966-af84-450c-b00a-57297115617a"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _mCPHeaderRepository.GetCountAsync(
                    code: "5a7e3a6b33b945fb9ede",
                    name: "e6590e92b9b3411c9e4ded3cc50dede46ecef02fe11440c3984d9a3ca26b2b775a43ef724ccb48a99c7b818cf82808c343e6eb28de444659b4b4a26d06fd417d5a7277203aef461b89161317ed4ddd9ce697ae712cef445690a8f2c649134e2292b4a8f7038a4e33aaed864bd0d9efb58cd07d5b2c1941e0ab6499cf6c224af"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}