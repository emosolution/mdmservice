using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IHolidayDetailsAppService _holidayDetailsAppService;
        private readonly IRepository<HolidayDetail, Guid> _holidayDetailRepository;

        public HolidayDetailsAppServiceTests()
        {
            _holidayDetailsAppService = GetRequiredService<IHolidayDetailsAppService>();
            _holidayDetailRepository = GetRequiredService<IRepository<HolidayDetail, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _holidayDetailsAppService.GetListAsync(new GetHolidayDetailsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.HolidayDetail.Id == Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4")).ShouldBe(true);
            result.Items.Any(x => x.HolidayDetail.Id == Guid.Parse("017061bc-8b2e-49c5-9442-4fd04573c3ef")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _holidayDetailsAppService.GetAsync(Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new HolidayDetailCreateDto
            {
                StartDate = new DateTime(2009, 4, 21),
                EndDate = new DateTime(2010, 1, 17),
                Description = "36e8c1ec7dcb4fb5a7de7f138114b73c29c3a9fc11d04563a13a07f2f5d778010093dc7f4ba44d02b9d3",
                HolidayId = Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756")
            };

            // Act
            var serviceResult = await _holidayDetailsAppService.CreateAsync(input);

            // Assert
            var result = await _holidayDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartDate.ShouldBe(new DateTime(2009, 4, 21));
            result.EndDate.ShouldBe(new DateTime(2010, 1, 17));
            result.Description.ShouldBe("36e8c1ec7dcb4fb5a7de7f138114b73c29c3a9fc11d04563a13a07f2f5d778010093dc7f4ba44d02b9d3");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new HolidayDetailUpdateDto()
            {
                StartDate = new DateTime(2006, 1, 2),
                EndDate = new DateTime(2002, 11, 10),
                Description = "20b85722681f4b67b122c7af67abf43dc15d1419fbb64",
                HolidayId = Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756")
            };

            // Act
            var serviceResult = await _holidayDetailsAppService.UpdateAsync(Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"), input);

            // Assert
            var result = await _holidayDetailRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.StartDate.ShouldBe(new DateTime(2006, 1, 2));
            result.EndDate.ShouldBe(new DateTime(2002, 11, 10));
            result.Description.ShouldBe("20b85722681f4b67b122c7af67abf43dc15d1419fbb64");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _holidayDetailsAppService.DeleteAsync(Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"));

            // Assert
            var result = await _holidayDetailRepository.FindAsync(c => c.Id == Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"));

            result.ShouldBeNull();
        }
    }
}