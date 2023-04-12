using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Companies;
using System;
using Volo.Abp.Application.Services;
using Volo.Abp;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Localization;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentsInternalAppService : ApplicationService, ICompanyIdentityUserAssignmentsInternalAppService
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly ICompaniesInternalAppService _companiesInternalAppService;

        public CompanyIdentityUserAssignmentsInternalAppService(
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository,
            ICompaniesInternalAppService companiesInternalAppService
            )
        {
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
            _companiesInternalAppService = companiesInternalAppService;

            LocalizationResource = typeof(MdmServiceResource);
        }

        [AllowAnonymous]
        public virtual async Task<CompanyDto> GetCurrentlySelectedCompanyAsync(Guid? inputIdentityUserId = null, 
            DateTime? checkTime = null)
        {
            DateTime time = checkTime == null ? DateTime.Now : (DateTime)checkTime;
            Guid? identityUserId = inputIdentityUserId == null ? CurrentUser.Id : (Guid) inputIdentityUserId;
            var assignments = 
                await _companyIdentityUserAssignmentRepository.GetListAsync(x =>
                    x.IdentityUserId == identityUserId);
            // if(CurrentUser is not null && CurrentUser.TenantId is null){
            //     return null;
            // }
            
            if (assignments.Count < 1)
            {
                throw new BusinessException(message: L["Error:CompanyIdentityUserAssignment:551"], code: "1");
            }
            CompanyDto selectedCompany;
            var selectedAssignment = assignments.OrderBy(x => x.CreationTime)
                .Where(x => x.CurrentlySelected == true).ToList();
            if (selectedAssignment.Count > 1)
            {
                throw new BusinessException(message: L["Error:CompanyIdentityUserAssignment:553"], code: "1");
            }
            else if (selectedAssignment.Count == 1)
            {
                Guid firstSelectedCompanyId = selectedAssignment.First().CompanyId;
                selectedCompany =
                        await _companiesInternalAppService.CheckActiveAsync(firstSelectedCompanyId, time, true);
            }
            else
            {
                var lastAssignment = assignments.First();
                Guid lastAssignedCompanyId = lastAssignment.CompanyId;
                selectedCompany =
                    await _companiesInternalAppService.CheckActiveAsync(lastAssignedCompanyId, time, true);

                lastAssignment.CurrentlySelected = true;
                await _companyIdentityUserAssignmentRepository.UpdateAsync(lastAssignment);
            }
            return selectedCompany;
        }
    }
}
