using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;

        public NumberingConfigDetailRepositoryTests()
        {
            _numberingConfigDetailRepository = GetRequiredService<INumberingConfigDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigDetailRepository.GetListAsync(x =>
                    x.Description == "cc3a9eb211c14d8dacb9711b77328dffffb8fc5232594fecbf00ef1588b07169942fdbc2df7a443f9b450c36855a1095f5b93d9ba4e74da1abe12fd6c6496430be97a6737a004a3a92d9c5a9565a0bb140cd7dcac452414fa54b717a871e800ea533a7610acf47fcb5b4749452fcc793bac49b30f29f4b85abe1ee90c5c08c7" &&
                    x.Prefix == "204e1edaca1b4ace9634" &&
                    x.Suffix == "901a3e1da6a845d280ac" &&
                    x.Active == true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9b4a47af-ef8d-4cb5-ba7d-48cb051e18ba"));
            });
        }
    }
}