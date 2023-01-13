using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial class CompaniesInternalAppService : ApplicationService, ICompaniesInternalAppService
    {
        //private readonly CompanyManager _companyManager;
        //private readonly IRepository<Company, Guid> _companyRepository;
        //private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyCustomRepository _companyCustomRepository;

        public CompaniesInternalAppService(
            //CompanyManager companyManager,
            //IRepository<Company, Guid> companyRepository,
            //ICompanyRepository companyRepository,
            ICompanyCustomRepository companyCustomRepository)
        {
            //_companyManager = companyManager;
            //_companyRepository = companyRepository;
            _companyCustomRepository = companyCustomRepository;
        }

        public async Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAndTenant(Guid identityUserId, Guid? tenantId)
        {
            Company companyHO = await _companyCustomRepository.GetHOCompanyFromIdentityUserAndTenant(identityUserId, tenantId);
            if (companyHO == null)
            {
                return null;
            }
            return ObjectMapper.Map<Company, CompanyWithTenantDto>(companyHO);
        }

        //public async Task<CompanyWithTenantDto> GetWithTenantIdAsynce(Guid id)
        //{
        //    Company company = await _companyRepository.GetAsync(id);
        //    return ObjectMapper.Map<Company, CompanyWithTenantDto>(company);
        //}
    }
}