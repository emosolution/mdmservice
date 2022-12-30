using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IUOMGroupDetailsAppService _uOMGroupDetailsAppService;
        private readonly IRepository<UOMGroupDetail, Guid> _uOMGroupDetailRepository;

        public UOMGroupDetailsAppServiceTests()
        {
            _uOMGroupDetailsAppService = GetRequiredService<IUOMGroupDetailsAppService>();
            _uOMGroupDetailRepository = GetRequiredService<IRepository<UOMGroupDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _uOMGroupDetailsAppService.GetListAsync(new GetUOMGroupDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.UOMGroupDetail.Id == Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62")).ShouldBe(true);
            result.Items.Any(x => x.UOMGroupDetail.Id == Guid.Parse("f852c863-a264-41ae-8658-d3d94df2bb22")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _uOMGroupDetailsAppService.GetAsync(Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UOMGroupDetailCreateDto
            {
                AltQty = 1532714443,
                BaseQty = 490425543,
                Active = true,
                UOMGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                AltUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                BaseUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _uOMGroupDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _uOMGroupDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AltQty.ToString().ShouldBe("1532714443");
            result.BaseQty.ToString().ShouldBe("490425543");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UOMGroupDetailUpdateDto()
            {
                AltQty = 1818769157,
                BaseQty = 734301699,
                Active = true,
                UOMGroupId = Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                AltUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                BaseUOMId = Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            };

            // Act
            var serviceResult = await _uOMGroupDetailsAppService.UpdateAsync(Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"), input);

            // Assert
            var result = await _uOMGroupDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AltQty.ToString().ShouldBe("1818769157");
            result.BaseQty.ToString().ShouldBe("734301699");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _uOMGroupDetailsAppService.DeleteAsync(Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"));

            // Assert
            var result = await _uOMGroupDetailRepository.FindAsync(c => c.Id == Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"));

            result.ShouldBeNull();
        }
    }
}