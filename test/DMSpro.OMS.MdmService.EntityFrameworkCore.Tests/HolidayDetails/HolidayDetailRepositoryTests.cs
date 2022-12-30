using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IHolidayDetailRepository _holidayDetailRepository;

        public HolidayDetailRepositoryTests()
        {
            _holidayDetailRepository = GetRequiredService<IHolidayDetailRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _holidayDetailRepository.GetListAsync(
                    description: "11a904b723024fb5a815e6"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _holidayDetailRepository.GetCountAsync(
                    description: "02391fc4c00e4cc38abd4163184066dc7e"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}