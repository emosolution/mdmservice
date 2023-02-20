using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using DMSpro.OMS.MdmService.ItemAttachments;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(MdmServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class MdmServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MdmServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MdmServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(ItemAttachmentCreateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(ItemAttachmentUpdateDto));
        });
    }
}
