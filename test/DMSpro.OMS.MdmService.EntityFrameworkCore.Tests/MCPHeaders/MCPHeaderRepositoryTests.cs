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
                    code: "a81b3cb09c174551a7d8",
                    name: "45ea898989144d088143a827498b16799a480162ca9343d28024a6540e2fe63a67dbfaa"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("67fe0da1-b355-4812-ac13-ef2b20992acc"));
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
                    code: "887957c52d62492789ce",
                    name: "d45f11acb702458ca4d2ebbcbc7db8175fd15fea9ee840db8e2ddc5f91bffc968019c2"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}