using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Web.Pages.CustomerGroupAttributes
{
    public class IndexModel : AbpPageModel
    {
        public string DescriptionFilter { get; set; }
        [SelectItems(nameof(CustomerGroupLookupList))]
        public Guid CustomerGroupIdFilter { get; set; }
        public List<SelectListItem> CustomerGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr0IdFilter { get; set; }
        public List<SelectListItem> CustomerAttributeValueLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr1IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr2IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr3IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr4IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr5IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr6IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr7IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr8IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr9IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr10IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr11IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr12IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr13IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr14IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr15IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr16IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr17IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr18IdFilter { get; set; }
        [SelectItems(nameof(CustomerAttributeValueLookupList))]
        public Guid? Attr19IdFilter { get; set; }

        private readonly ICustomerGroupAttributesAppService _customerGroupAttributesAppService;

        public IndexModel(ICustomerGroupAttributesAppService customerGroupAttributesAppService)
        {
            _customerGroupAttributesAppService = customerGroupAttributesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerGroupLookupList.AddRange((
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
    }
}