using DMSpro.OMS.MdmService.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.Web.Pages.CompanyIdentityUserAssignments
{
    public class EditModalModel : MdmServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CompanyIdentityUserAssignmentUpdateViewModel CompanyIdentityUserAssignment { get; set; }

        public List<SelectListItem> CompanyLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;

        public EditModalModel(ICompanyIdentityUserAssignmentsAppService companyIdentityUserAssignmentsAppService)
        {
            _companyIdentityUserAssignmentsAppService = companyIdentityUserAssignmentsAppService;
        }

        public async Task OnGetAsync()
        {
            var companyIdentityUserAssignmentWithNavigationPropertiesDto = await _companyIdentityUserAssignmentsAppService.GetWithNavigationPropertiesAsync(Id);
            CompanyIdentityUserAssignment = ObjectMapper.Map<CompanyIdentityUserAssignmentDto, CompanyIdentityUserAssignmentUpdateViewModel>(companyIdentityUserAssignmentWithNavigationPropertiesDto.CompanyIdentityUserAssignment);

            CompanyLookupListRequired.AddRange((
                                    await _companyIdentityUserAssignmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _companyIdentityUserAssignmentsAppService.UpdateAsync(Id, ObjectMapper.Map<CompanyIdentityUserAssignmentUpdateViewModel, CompanyIdentityUserAssignmentUpdateDto>(CompanyIdentityUserAssignment));
            return NoContent();
        }
    }

    public class CompanyIdentityUserAssignmentUpdateViewModel : CompanyIdentityUserAssignmentUpdateDto
    {
    }
}