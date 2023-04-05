using DMSpro.OMS.MdmService.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupAttributes
{
    public class EditModalModel : MdmServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerGroupAttributeUpdateViewModel CustomerGroupAttribute { get; set; }

        public List<SelectListItem> CustomerGroupLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerGroupAttributesAppService _customerGroupAttributesAppService;

        public EditModalModel(ICustomerGroupAttributesAppService customerGroupAttributesAppService)
        {
            _customerGroupAttributesAppService = customerGroupAttributesAppService;
        }

        public async Task OnGetAsync()
        {
            var customerGroupAttributeWithNavigationPropertiesDto = await _customerGroupAttributesAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerGroupAttribute = ObjectMapper.Map<CustomerGroupAttributeDto, CustomerGroupAttributeUpdateViewModel>(customerGroupAttributeWithNavigationPropertiesDto.CustomerGroupAttribute);

            CustomerGroupLookupListRequired.AddRange((
                                    await _customerGroupAttributesAppService.GetCustomerGroupLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CustomerAttributeValueLookupList.AddRange((
                                    await _customerGroupAttributesAppService.GetCustomerAttributeValueLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerGroupAttributesAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerGroupAttributeUpdateViewModel, CustomerGroupAttributeUpdateDto>(CustomerGroupAttribute));
            return NoContent();
        }
    }

    public class CustomerGroupAttributeUpdateViewModel : CustomerGroupAttributeUpdateDto
    {
    }
}