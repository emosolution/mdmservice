using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DMSpro.OMS.MdmService.Web.Pages;

/* Inherit your PageModel classes from this class. */
public abstract class MdmServicePageModel : AbpPageModel
{
    protected MdmServicePageModel()
    {
        LocalizationResourceType = typeof(MdmServiceResource);
        ObjectMapperContext = typeof(MdmServiceWebModule);
    }
}
