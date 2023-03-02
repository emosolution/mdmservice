using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPriceUpdateRepository _priceUpdateRepository;

        public PriceUpdateRepositoryTests()
        {
            _priceUpdateRepository = GetRequiredService<IPriceUpdateRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceUpdateRepository.GetListAsync(
                    code: "1cf7c827627a48618862",
                    description: "1895bd57b14c4cebbbd8366989cf08fd26b4575473c24dfe8348084700b9800a500905f3d21b47bf8f96899168ba29a2449e4b3552054064abf3b941088fc0604d6a23465a0347638e379b436b3c8ec168e065d14ce04f4a964b75ca56721579ab1a344161724b15bb2e330cee2e2a6d04914e8c2bee4664b4c8833cb6b14b8632ab98c2f0ee43d4960b3cde358465a0fc36423d55634924a18a0e4e42ac60a588a301f59d5a49d78ddb5d1fcdd265e4d7e278892c3242d7a3abd893e05a565f5f450d4e30f84fafb5f5e5fe93b92222888c66c1c0e04968ad18b2fc5a7a8b703ddd993f73074949a799f390ac32ba2e0ac89eb8474b4c25a30f",
                    status: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _priceUpdateRepository.GetCountAsync(
                    code: "78c4bbf758d94f29a590",
                    description: "36a5675141364a9bb6694ea8271bcd055125f25e839e4f87aad79373b1367355857eebcdc53c43048a4571124d7eba02a1b2ce0fe5b44ab1927e22a580fe3a3b88948e042b354fbd82ed3aa278ef09ec93eb5b47c23d40b69f4c189e85df1814ceddf05c5a1f4d43b57167538c4def72871ca020101f4fa096ffb6c7db3a81e0214373e229de4c608f1e73245403a2eb77f23b15be1346a6860bfe41763cb476c8342ec0364e47689072b6fdb55335a7959de479e5994bc6a14e95d8cc227008207c8b559cdd4d258f5606f7efaaca39c93b465931e34f9487dcb7f1b540dae18805c1b5c4d24e32a1b76db3f40379ff30a11eb632064fe0bafe",
                    status: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}