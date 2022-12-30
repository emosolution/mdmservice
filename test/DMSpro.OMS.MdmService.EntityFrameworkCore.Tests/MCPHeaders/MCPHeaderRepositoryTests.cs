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
                    code: "9cbbdc5f3557449c98df",
                    name: "6e21c9cc5e574113b1"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c01415d9-e009-42ed-84c7-54d857286730"));
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
                    code: "75ddc503b6b347ddba91",
                    name: "daf4b0b576c549e9b98a4108968000a5879567d8666749eb9170a47a43012517da080b"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}