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
            result.Items.Any(x => x.CompanyInZone.Id == Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420")).ShouldBe(true);
            result.Items.Any(x => x.CompanyInZone.Id == Guid.Parse("419472a0-78eb-4bae-b9af-ebc2c8122115")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companyInZonesAppService.GetAsync(Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyInZoneCreateDto
            {
                EffectiveDate = new DateTime(2010, 3, 9),
                EndDate = new DateTime(2008, 4, 3),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.CreateAsync(input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2010, 3, 9));
            result.EndDate.ShouldBe(new DateTime(2008, 4, 3));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyInZoneUpdateDto()
            {
                EffectiveDate = new DateTime(2020, 6, 21),
                EndDate = new DateTime(2012, 6, 25),
                SalesOrgHierarchyId = Guid.Parse("93dbf4fd-a1a6-4803-9cfe-913575f46e05"),
                CompanyId = Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557")
            };

            // Act
            var serviceResult = await _companyInZonesAppService.UpdateAsync(Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"), input);

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EffectiveDate.ShouldBe(new DateTime(2020, 6, 21));
            result.EndDate.ShouldBe(new DateTime(2012, 6, 25));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companyInZonesAppService.DeleteAsync(Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"));

            // Assert
            var result = await _companyInZoneRepository.FindAsync(c => c.Id == Guid.Parse("08b30c2c-e937-4587-9680-14f5a2916420"));

            result.ShouldBeNull();
        }
    }
}