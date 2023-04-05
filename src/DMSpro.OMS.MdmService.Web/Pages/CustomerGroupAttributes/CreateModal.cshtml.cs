using DMSpro.OMS.MdmService.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupAttributes
{
    public class CreateModalModel : MdmServicePageModel
    {
        [BindProperty]
        public CustomerGroupAttributeCreateViewModel CustomerGroupAttribute { get; set; }

        public List<SelectListItem> CustomerGroupLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerGroupAttributesAppService _customerGroupAttributesAppService;

        public CreateModalModel(ICustomerGroupAttributesAppService customerGroupAttributesAppService)
        {
            _customerGroupAttributesAppService = customerGroupAttributesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerGroupAttribute = new CustomerGroupAttributeCreateViewModel();
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

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerGroupAttributesAppService.CreateAsync(ObjectMapper.Map<CustomerGroupAttributeCreateViewModel, CustomerGroupAttributeCreateDto>(CustomerGroupAttribute));
            return NoContent();
        }
    }

    public class CustomerGroupAttributeCreateViewModel : CustomerGroupAttributeCreateDto
    {
    }
}