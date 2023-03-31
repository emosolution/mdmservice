using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZonesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICompanyInZonesAppService _companyInZonesAppService;
        private readonly IRepository<CompanyInZone, Guid> _companyInZoneRepository;

        public CompanyInZonesAppServiceTests()
        {
            _companyInZonesAppService = GetRequiredService<ICompanyInZonesAppService>();
            _companyInZoneRepository = GetRequiredService<IRepository<CompanyInZone, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companyInZonesAppService.GetAsync(Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyInZoneCreateDto
            {
                EffectiveDate = new DateTime(2021, 7, 13),
                EndDate = new DateTime(2009, 8, 3),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2021, 7, 13));
            result.EndDate.ShouldBe(new DateTime(2009, 8, 3));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2006, 3, 4),
                EndDate = new DateTime(2012, 2, 21),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                ItemGroupId = Guid.Parse("13208751-3cd3-4b59-b410-4a28a1b9022f")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.UpdateAsync(Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"), input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2006, 3, 4));
            result.EndDate.ShouldBe(new DateTime(2012, 2, 21));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyInZonesAppService.DeleteAsync(Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"));

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"));

            result.ShouldBeNull();
        }
    }
}