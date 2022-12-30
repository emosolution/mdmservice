using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService;

public abstract class MdmServiceAppService : ApplicationService
{
    protected MdmServiceAppService()
    {
        LocalizationResource = typeof(MdmServiceResource);
        ObjectMapperContext = typeof(MdmServiceApplicationModule);
    }
}
