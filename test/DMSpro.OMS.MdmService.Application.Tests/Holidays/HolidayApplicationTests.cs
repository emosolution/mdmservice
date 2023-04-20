using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidaysAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IHolidaysAppService _holidaysAppService;
        private readonly IRepository<Holiday, Guid> _holidayRepository;

        public HolidaysAppServiceTests()
        {
            _holidaysAppService = GetRequiredService<IHolidaysAppService>();
            _holidayRepository = GetRequiredService<IRepository<Holiday, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _holidaysAppService.GetAsync(Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new HolidayCreateDto
            {
                Year = 2090,
                Description = "581ecfed352b4fb984c20e8d94debbd1b30694e6c354"
            };

            // Act
            var serviceResult = await _holidaysAppService.CreateAsync(input);

            // Assert
            var result = await _holidayRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Year.ShouldBe(2090);
            result.Description.ShouldBe("581ecfed352b4fb984c20e8d94debbd1b30694e6c354");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new HolidayUpdateDto()
            {
                Year = 2091,
                Description = "b368e1dfac024b5ba3e57184e1c6858835ae23027856406096c4a6e"
            };

            // Act
            var serviceResult = await _holidaysAppService.UpdateAsync(Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"), input);

            // Assert
            var result = await _holidayRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Year.ShouldBe(2091);
            result.Description.ShouldBe("b368e1dfac024b5ba3e57184e1c6858835ae23027856406096c4a6e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _holidaysAppService.DeleteAsync(Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"));

            // Assert
            var result = await _holidayRepository.FindAsync(c => c.Id == Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"));

            result.ShouldBeNull();
        }
    }
}