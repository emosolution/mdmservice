using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesChannels;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public class SalesChannelRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesChannelRepository _salesChannelRepository;

        public SalesChannelRepositoryTests()
        {
            _salesChannelRepository = GetRequiredService<ISalesChannelRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesChannelRepository.GetListAsync(
                    code: "be770a8a8a6a40898147",
                    name: "8f894fa39f294440a112e3ed48bc199f6df5740793194999babe5bcf0c0317630191daa78c344b9eacc630f7c9a6efaf16f0f264b0f046b2a56f53239de79aa95ad3510b5ea84a50851b1ae0d42daa96db8c106854b94f22a879e789fa7ec2f23cf05e39",
                    description: "c07c1972d3e14d798cc00",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("38ef75bc-06cf-4ebb-8b19-56002525a710"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesChannelRepository.GetCountAsync(
                    code: "1b0f403755bc4b489581",
                    name: "24ba188aa6ca4621818235dbee8235ae9b3812dfaf7f4ddba8b94526b2224d20bb78bc0008bd4d0eb5010c7380cb2e8bebc16ec9bbcb471ba0e447ab55aa576d8ffc711c1298472193edc6905e0e97a119335983fa9148aa847d8855da4f5a4b3e963077",
                    description: "534e4789bc4543579cffec5e2922bf2a4d09a3a7d7cc49ccb0801ff610ff08b505cd53174cd44c57ae12",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}