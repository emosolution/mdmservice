using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;

        public UOMGroupDetailRepositoryTests()
        {
            _uOMGroupDetailRepository = GetRequiredService<IUOMGroupDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _uOMGroupDetailRepository.GetListAsync(
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _uOMGroupDetailRepository.GetCountAsync(
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}