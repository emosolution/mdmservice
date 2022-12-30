using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public class SSHistoryInZonesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISSHistoryInZonesAppService _sSHistoryInZonesAppService;
        private readonly IRepository<SSHistoryInZone, Guid> _sSHistoryInZoneRepository;

        public SSHistoryInZonesAppServiceTests()
        {
            _sSHistoryInZonesAppService = GetRequiredService<ISSHistoryInZonesAppService>();
            _sSHistoryInZoneRepository = GetRequiredService<IRepository<SSHistoryInZone, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _sSHistoryInZonesAppService.GetListAsync(new GetSSHistoryInZonesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.SSHistoryInZone.Id == Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa")).ShouldBe(true);
            result.Items.Any(x => x.SSHistoryInZone.Id == Guid.Parse("5075557b-0bc5-4f04-865a-37b70e258503")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _sSHistoryInZonesAppService.GetAsync(Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SSHistoryInZoneCreateDto
            {
                EffectiveDate = new DateTime(2008, 8, 4),
                EndDate = new DateTime(2006, 2, 14),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _sSHistoryInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _sSHistoryInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2008, 8, 4));
            result.EndDate.ShouldBe(new DateTime(2006, 2, 14));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SSHistoryInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2005, 11, 14),
                EndDate = new DateTime(2012, 3, 25),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                EmployeeId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _sSHistoryInZonesAppService.UpdateAsync(Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"), input);

            // Assert
            var result = await _sSHistoryInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2005, 11, 14));
            result.EndDate.ShouldBe(new DateTime(2012, 3, 25));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _sSHistoryInZonesAppService.DeleteAsync(Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"));

            // Assert
            var result = await _sSHistoryInZoneRepository.FindAsync(c => c.Id == Guid.Parse("6628ad79-717b-492a-9be6-13711bdfdafa"));

            result.ShouldBeNull();
        }
    }
}