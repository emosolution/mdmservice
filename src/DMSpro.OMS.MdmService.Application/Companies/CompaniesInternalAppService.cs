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
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public CompaniesInternalAppService(
            ICompanyRepository companyRepository,
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
        {
            _companyRepository = companyRepository;
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAsync(Guid identityUserId, Guid? tenantId)
        {
            try
            {
                Company companyHO = await _companyRepository.GetHOCompanyFromIdentityUserAsync(identityUserId, tenantId);
                return ObjectMapper.Map<Company, CompanyWithTenantDto>(companyHO);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }

        }

        public async Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUserAsync(Guid companyId, 
            Guid identityUserId, Guid? tenantId)
        {
            List<CompanyIdentityUserAssignmentWithNavigationProperties> assignments =
                await _companyIdentityUserAssignmentRepository.GetListWithNavigationPropertiesAsync(
                    identityUserId: identityUserId, companyId: companyId);
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