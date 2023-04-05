using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerAttributeValues
{
    public class IndexModel : AbpPageModel
    {
        public string CodeFilter { get; set; }
        public string AttrValNameFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeLookupList))]
        public Guid CustomerAttributeIdFilter { get; set; }
        public List<SelectListItem> CustomerAttributeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? ParentIdFilter { get; set; }
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly ICustomerAttributeValuesAppService _customerAttributeValuesAppService;

        public IndexModel(ICustomerAttributeValuesAppService customerAttributeValuesAppService)
        {
            _customerAttributeValuesAppService = customerAttributeValuesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerAttributeLookupList.AddRange((
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
    }
}