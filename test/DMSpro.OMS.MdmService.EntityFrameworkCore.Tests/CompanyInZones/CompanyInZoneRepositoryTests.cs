using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public class CompanyInZoneRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICompanyInZoneRepository _companyInZoneRepository;

        public CompanyInZoneRepositoryTests()
        {
            _companyInZoneRepository = GetRequiredService<ICompanyInZoneRepository>();
        }
    }
}