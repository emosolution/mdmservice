using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayRepositoryTests()
        {
            _holidayRepository = GetRequiredService<IHolidayRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _holidayRepository.GetListAsync(
                    description: "628145de1e984f499"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _holidayRepository.GetCountAsync(
                    description: "486069a0ab8a4979a526a8d6b924fdfdd34ff825a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}