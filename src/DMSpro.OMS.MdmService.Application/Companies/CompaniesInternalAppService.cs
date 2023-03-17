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

        public async Task<CompanyDto> CheckActiveAsync(Guid id, DateTime? checkingDate,
            bool throwErrorOnInactive = false)
        {
            checkingDate = checkingDate == null ? DateTime.Now : (DateTime)checkingDate;
            try
            {
                var record = await _companyRepository.GetAsync(
                    x => x.Id == id && x.Active == true &&
                    x.EffectiveDate < checkingDate &&
                    (x.EndDate == null || x.EndDate >= checkingDate));
                return ObjectMapper.Map<Company, CompanyDto>(record);
            }
            catch (EntityNotFoundException)
            {
                if (throwErrorOnInactive)
                {
                    throw new BusinessException(message: L["Error:CompaniesAppService:550"], code: "1");
                }
                return null;
            }
        }
    }
}