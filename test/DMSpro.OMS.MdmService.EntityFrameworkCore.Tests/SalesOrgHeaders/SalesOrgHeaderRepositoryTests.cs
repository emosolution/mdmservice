using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

        public SalesOrgHeaderRepositoryTests()
        {
            _salesOrgHeaderRepository = GetRequiredService<ISalesOrgHeaderRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHeaderRepository.GetListAsync(
                    code: "7efb90e3416f417d8b32",
                    name: "a5b3e11d58e04aec8c177017c7cf1dc424c29a1f02574944a7d1c6f758e3b7a19fa52c8",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHeaderRepository.GetCountAsync(
                    code: "c1ed60a7dee949ce9441",
                    name: "854ee42703ce4e29863ce9691dcca6dc71379046b2e04f3d91bba8f71c62c44ad86781c8300e46ecac4f99",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}