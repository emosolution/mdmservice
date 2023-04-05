using DMSpro.OMS.MdmService.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerAttributeValues
{
    public class CreateModalModel : MdmServicePageModel
    {
        [BindProperty]
        public CustomerAttributeValueCreateViewModel CustomerAttributeValue { get; set; }

        public List<SelectListItem> CustomerAttributeLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerAttributeValuesAppService _customerAttributeValuesAppService;

        public CreateModalModel(ICustomerAttributeValuesAppService customerAttributeValuesAppService)
        {
            _customerAttributeValuesAppService = customerAttributeValuesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerAttributeValue = new CustomerAttributeValueCreateViewModel();
            CustomerAttributeLookupListRequired.AddRange((
                                    await _customerAttributeValuesAppService.GetCustomerAttributeLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CustomerAttributeValueLookupList.AddRange((
                                    await _customerAttributeValuesAppService.GetCustomerAttributeValueLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerAttributeValuesAppService.CreateAsync(ObjectMapper.Map<CustomerAttributeValueCreateViewModel, CustomerAttributeValueCreateDto>(CustomerAttributeValue));
            return NoContent();
        }
    }

    public class CustomerAttributeValueCreateViewModel : CustomerAttributeValueCreateDto
    {
    }
}