using DMSpro.OMS.MdmService.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerGroupGeos;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupGeos
{
    public class CreateModalModel : MdmServicePageModel
    {
        [BindProperty]
        public CustomerGroupGeoCreateViewModel CustomerGroupGeo { get; set; }

        public List<SelectListItem> CustomerGroupLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> GeoMasterLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;

        public CreateModalModel(ICustomerGroupGeosAppService customerGroupGeosAppService)
        {
            _customerGroupGeosAppService = customerGroupGeosAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerGroupGeo = new CustomerGroupGeoCreateViewModel();
            CustomerGroupLookupListRequired.AddRange((
                                    await _customerGroupGeosAppService.GetCustomerGroupLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            GeoMasterLookupListRequired.AddRange((
                                    await _customerGroupGeosAppService.GetGeoMasterLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerGroupGeosAppService.CreateAsync(ObjectMapper.Map<CustomerGroupGeoCreateViewModel, CustomerGroupGeoCreateDto>(CustomerGroupGeo));
            return NoContent();
        }
    }

    public class CustomerGroupGeoCreateViewModel : CustomerGroupGeoCreateDto
    {
    }
}