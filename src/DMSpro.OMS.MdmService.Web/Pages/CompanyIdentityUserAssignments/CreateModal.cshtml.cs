using DMSpro.OMS.MdmService.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.Web.Pages.CompanyIdentityUserAssignments
{
    public class CreateModalModel : MdmServicePageModel
    {
        [BindProperty]
        public CompanyIdentityUserAssignmentCreateViewModel CompanyIdentityUserAssignment { get; set; }

        public List<SelectListItem> CompanyLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;

        public CreateModalModel(ICompanyIdentityUserAssignmentsAppService companyIdentityUserAssignmentsAppService)
        {
            _companyIdentityUserAssignmentsAppService = companyIdentityUserAssignmentsAppService;
        }

        public async Task OnGetAsync()
        {
            CompanyIdentityUserAssignment = new CompanyIdentityUserAssignmentCreateViewModel();
            CompanyLookupListRequired.AddRange((
                                    await _companyIdentityUserAssignmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _companyIdentityUserAssignmentsAppService.CreateAsync(ObjectMapper.Map<CompanyIdentityUserAssignmentCreateViewModel, CompanyIdentityUserAssignmentCreateDto>(CompanyIdentityUserAssignment));
            return NoContent();
        }
    }

    public class CompanyIdentityUserAssignmentCreateViewModel : CompanyIdentityUserAssignmentCreateDto
    {
    }
}