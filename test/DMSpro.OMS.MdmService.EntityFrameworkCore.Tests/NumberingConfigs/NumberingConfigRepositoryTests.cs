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
                    prefix: "0b213f7643da4d708d51",
                    suffix: "5b8fa92949694ab69be0",
                    active: true,
                    description: "9ecd5e4fccd241d68575831b51618a3977d00ffb8dcc4b61be1586919dacae10e8d66b3927e74369b68ed3af6305591e8c4df0387b2f4d29bdc262637d697e0cdfaa46f36a284a72b70fe7ca417d80550b2b7aaf6c804debaee0d732df24e4a1f9da6f9ff7894ba5809183a607ef6c7226246c4c11974b2b9f5e3cd686c94c3"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"));
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
                    prefix: "e988d203e2fc4e2a9bac",
                    suffix: "5ff3d326937d42e9b8d6",
                    active: true,
                    description: "36fe5ac000fe4917af3fdbacf42751e4d577e247dca24e8895add2b3c4c723bd0a920079a1814b33a09c6a8664077c2f5f557b862efc4128be50349089f7eae14d1b354bbe8d47d8b3e8f258430a6142fd2f8296b94347409a0c9394a9e530db5532b393287f4d94ba67df0aba3be5df2576d8b1763746a2b79e990180284a1"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}