using DMSpro.OMS.MdmService.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupGeos;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupGeos
{
    public class EditModalModel : MdmServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerGroupGeoUpdateViewModel CustomerGroupGeo { get; set; }

        public List<SelectListItem> CustomerGroupLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> GeoMasterLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;

        public EditModalModel(ICustomerGroupGeosAppService customerGroupGeosAppService)
        {
            _customerGroupGeosAppService = customerGroupGeosAppService;
        }

        public async Task OnGetAsync()
        {
            var customerGroupGeoWithNavigationPropertiesDto = await _customerGroupGeosAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerGroupGeo = ObjectMapper.Map<CustomerGroupGeoDto, CustomerGroupGeoUpdateViewModel>(customerGroupGeoWithNavigationPropertiesDto.CustomerGroupGeo);

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

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerGroupGeosAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerGroupGeoUpdateViewModel, CustomerGroupGeoUpdateDto>(CustomerGroupGeo));
            return NoContent();
        }
    }

    public class CustomerGroupGeoUpdateViewModel : CustomerGroupGeoUpdateDto
    {
    }
}