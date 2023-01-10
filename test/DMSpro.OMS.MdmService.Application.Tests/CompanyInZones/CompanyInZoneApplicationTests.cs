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
        public async Task GetListAsync()
        {
            // Act
            var result = await _companyInZonesAppService.GetListAsync(new GetCompanyInZonesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CompanyInZone.Id == Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb")).ShouldBe(true);
            result.Items.Any(x => x.CompanyInZone.Id == Guid.Parse("5d00f7ed-c7a2-414d-9aaf-a0e9a518cedd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companyInZonesAppService.GetAsync(Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyInZoneCreateDto
            {
                EffectiveDate = new DateTime(2017, 8, 13),
                EndDate = new DateTime(2008, 6, 11),
                IsBase = true,
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2017, 8, 13));
            result.EndDate.ShouldBe(new DateTime(2008, 6, 11));
            result.IsBase.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2021, 10, 19),
                EndDate = new DateTime(2018, 8, 22),
                IsBase = true,
                SalesOrgHierarchyId = Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.UpdateAsync(Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"), input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2021, 10, 19));
            result.EndDate.ShouldBe(new DateTime(2018, 8, 22));
            result.IsBase.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyInZonesAppService.DeleteAsync(Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"));

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == Guid.Parse("c1702429-6343-45e0-bf7d-03cb29fa4beb"));

            result.ShouldBeNull();
        }
    }
}