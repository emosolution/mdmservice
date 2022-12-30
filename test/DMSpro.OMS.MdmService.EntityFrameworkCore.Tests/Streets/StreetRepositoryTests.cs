using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Streets;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IStreetRepository _streetRepository;

        public StreetRepositoryTests()
        {
            _streetRepository = GetRequiredService<IStreetRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _streetRepository.GetListAsync(
                    name: "ZQ}qYgszus|kl~qss^qGo4jqTpG|9us"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _streetRepository.GetCountAsync(
                    name: "rv}uy[j|mspum}s^uuqmswi"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}