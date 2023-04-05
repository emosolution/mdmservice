using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupGeos
{
    public class IndexModel : AbpPageModel
    {
        public string DescriptionFilter { get; set; }
        [SelectItems(nameof(ActiveBoolFilterItems))]
        public string ActiveFilter { get; set; }

        public List<SelectListItem> ActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(CustomerGroupLookupList))]
        public Guid CustomerGroupIdFilter { get; set; }
        public List<SelectListItem> CustomerGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(GeoMasterLookupList))]
        public Guid GeoMaster0IdFilter { get; set; }
        public List<SelectListItem> GeoMasterLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(GeoMasterLookupList))]
        public Guid GeoMaster1IdFilter { get; set; }
        [SelectItems(nameof(GeoMasterLookupList))]
        public Guid GeoMaster2IdFilter { get; set; }
        [SelectItems(nameof(GeoMasterLookupList))]
        public Guid GeoMaster3IdFilter { get; set; }
        [SelectItems(nameof(GeoMasterLookupList))]
        public Guid GeoMaster4IdFilter { get; set; }

        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;

        public IndexModel(ICustomerGroupGeosAppService customerGroupGeosAppService)
        {
            _customerGroupGeosAppService = customerGroupGeosAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerGroupLookupList.AddRange((
                    await _customerGroupGeosAppService.GetCustomerGroupLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            GeoMasterLookupList.AddRange((
                            await _customerGroupGeosAppService.GetGeoMasterLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}