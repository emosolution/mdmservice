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
        private readonly ICompanyCustomRepository _companyCustomRepository;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public CompaniesInternalAppService(
            ICompanyCustomRepository companyCustomRepository,
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
        {
            _companyCustomRepository = companyCustomRepository;
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUser(Guid identityUserId, Guid? tenantId)
        {
            try
            {
                Company companyHO = await _companyCustomRepository.GetHOCompanyFromIdentityUser(identityUserId, tenantId);
                return ObjectMapper.Map<Company, CompanyWithTenantDto>(companyHO);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }

        }

        public async Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUser(Guid companyId, Guid identityUserId, Guid? tenantId)
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

    }
}