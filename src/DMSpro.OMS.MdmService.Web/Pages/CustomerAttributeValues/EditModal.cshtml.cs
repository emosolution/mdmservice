using DMSpro.OMS.MdmService.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerAttributeValues
{
    public class EditModalModel : MdmServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerAttributeValueUpdateViewModel CustomerAttributeValue { get; set; }

        public List<SelectListItem> CustomerAttributeLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerAttributeValuesAppService _customerAttributeValuesAppService;

        public EditModalModel(ICustomerAttributeValuesAppService customerAttributeValuesAppService)
        {
            _customerAttributeValuesAppService = customerAttributeValuesAppService;
        }

        public async Task OnGetAsync()
        {
            var customerAttributeValueWithNavigationPropertiesDto = await _customerAttributeValuesAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerAttributeValue = ObjectMapper.Map<CustomerAttributeValueDto, CustomerAttributeValueUpdateViewModel>(customerAttributeValueWithNavigationPropertiesDto.CustomerAttributeValue);

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

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerAttributeValuesAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerAttributeValueUpdateViewModel, CustomerAttributeValueUpdateDto>(CustomerAttributeValue));
            return NoContent();
        }
    }

    public class CustomerAttributeValueUpdateViewModel : CustomerAttributeValueUpdateDto
    {
    }
}