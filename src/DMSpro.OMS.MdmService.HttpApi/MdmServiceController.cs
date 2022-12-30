using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DMSpro.OMS.MdmService;

public abstract class MdmServiceController : AbpControllerBase
{
    protected MdmServiceController()
    {
        LocalizationResource = typeof(MdmServiceResource);
    }
}
