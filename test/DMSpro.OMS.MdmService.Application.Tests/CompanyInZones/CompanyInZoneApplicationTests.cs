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
            var result = await _companyInZonesAppService.GetAsync(Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyInZoneCreateDto
            {
                EffectiveDate = new DateTime(2019, 3, 14),
                EndDate = new DateTime(2007, 5, 11),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _companyInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 3, 14));
            result.EndDate.ShouldBe(new DateTime(2007, 5, 11));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2004, 1, 7),
                EndDate = new DateTime(2022, 6, 23),
                SalesOrgHierarchyId = Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"),
                CompanyId = Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),

            };

            // Act
            var serviceResult = await _companyInZonesAppService.UpdateAsync(Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"), input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2004, 1, 7));
            result.EndDate.ShouldBe(new DateTime(2022, 6, 23));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyInZonesAppService.DeleteAsync(Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"));

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == Guid.Parse("18cf812e-4e0b-491e-828a-a632eee53480"));

            result.ShouldBeNull();
        }
    }
}