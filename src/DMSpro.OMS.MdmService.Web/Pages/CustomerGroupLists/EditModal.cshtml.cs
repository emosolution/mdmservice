using DMSpro.OMS.MdmService.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupLists;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupLists
{
    public class EditModalModel : MdmServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerGroupListUpdateViewModel CustomerGroupList { get; set; }

        public List<SelectListItem> CustomerLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> CustomerGroupLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };

        private readonly ICustomerGroupListsAppService _customerGroupListsAppService;

        public EditModalModel(ICustomerGroupListsAppService customerGroupListsAppService)
        {
            _customerGroupListsAppService = customerGroupListsAppService;
        }

        public async Task OnGetAsync()
        {
            var customerGroupListWithNavigationPropertiesDto = await _customerGroupListsAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerGroupList = ObjectMapper.Map<CustomerGroupListDto, CustomerGroupListUpdateViewModel>(customerGroupListWithNavigationPropertiesDto.CustomerGroupList);

            CustomerLookupListRequired.AddRange((
                                    await _customerGroupListsAppService.GetCustomerLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CustomerGroupLookupListRequired.AddRange((
                                    await _customerGroupListsAppService.GetCustomerGroupLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerGroupListsAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerGroupListUpdateViewModel, CustomerGroupListUpdateDto>(CustomerGroupList));
            return NoContent();
        }
    }

    public class CustomerGroupListUpdateViewModel : CustomerGroupListUpdateDto
    {
    }
}