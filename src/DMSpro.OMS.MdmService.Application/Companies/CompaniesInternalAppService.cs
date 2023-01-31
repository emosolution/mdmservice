using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompaniesInternalAppService : ApplicationService, ICompaniesInternalAppService
    {
        //private readonly CompanyManager _companyManager;
        //private readonly IRepository<Company, Guid> _companyRepository;
        //private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyCustomRepository _companyCustomRepository;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public CompaniesInternalAppService(
            //CompanyManager companyManager,
            //IRepository<Company, Guid> companyRepository,
            //ICompanyRepository companyRepository,
            ICompanyCustomRepository companyCustomRepository,
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
        {
            //_companyManager = companyManager;
            //_companyRepository = companyRepository;
            _companyCustomRepository = companyCustomRepository;
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAndTenant(Guid identityUserId, Guid? tenantId)
        {
            try
            {
                Company companyHO = await _companyCustomRepository.GetHOCompanyFromIdentityUserAndTenant(identityUserId, tenantId);
                return ObjectMapper.Map<Company, CompanyWithTenantDto>(companyHO);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }

        }

        public async Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUserAndTenant(Guid companyId, Guid identityUserId, Guid? tenantId)
        {
            List<CompanyIdentityUserAssignmentWithNavigationProperties> assignments =
                await _companyIdentityUserAssignmentRepository.GetListWithNavigationPropertiesAsync(identityUserId: identityUserId, companyId: companyId);
            if (assignments.Count != 1)
            {
                return null;
            }
            Company company = assignments[0].Company;
            if (company.TenantId != tenantId)
            {
                return null;
            }
            return ObjectMapper.Map<Company, CompanyWithTenantDto>(company);
        }

        //public async Task<CompanyWithTenantDto> GetWithTenantIdAsynce(Guid id)
        //{
        //    Company company = await _companyRepository.GetAsync(id);
        //    return ObjectMapper.Map<Company, CompanyWithTenantDto>(company);
        //}
    }
}