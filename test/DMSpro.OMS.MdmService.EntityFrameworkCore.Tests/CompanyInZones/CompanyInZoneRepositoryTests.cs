using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICompanyInZoneRepository _companyInZoneRepository;

        public CompanyInZoneRepositoryTests()
        {
            _companyInZoneRepository = GetRequiredService<ICompanyInZoneRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyInZoneRepository.GetListAsync(

                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e982da88-da0d-465e-9261-439f600ea491"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyInZoneRepository.GetCountAsync(

                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}