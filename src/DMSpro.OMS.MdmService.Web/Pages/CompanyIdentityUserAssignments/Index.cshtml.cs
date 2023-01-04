using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Web.Pages.CompanyIdentityUserAssignments
{
    public class IndexModel : AbpPageModel
    {
        public string IdentityUserIdFilter { get; set; }
        [SelectItems(nameof(CompanyLookupList))]
        public Guid CompanyIdFilter { get; set; }
        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;

        public IndexModel(ICompanyIdentityUserAssignmentsAppService companyIdentityUserAssignmentsAppService)
        {
            _companyIdentityUserAssignmentsAppService = companyIdentityUserAssignmentsAppService;
        }

        public async Task OnGetAsync()
        {
            CompanyLookupList.AddRange((
                    await _companyIdentityUserAssignmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}