using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IVATRepository _vATRepository;

        public VATRepositoryTests()
        {
            _vATRepository = GetRequiredService<IVATRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vATRepository.GetListAsync(
                    code: "47a28fbd2e7f499b917d",
                    name: "a09078b4dcb446afa68accd69d3297aaf7fad3001b1f4ba2a001c375e4db1437ece485dc8bb5453c86ea4afa53baa4b91843"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _vATRepository.GetCountAsync(
                    code: "69272ab7432d4e27950e",
                    name: "956b3c0b94f74cc09141737b09fe5bc023e0da39b3804fb6b338748183b15b8fe719b9c798d8488c89a4ef8c848f2dca3108"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}