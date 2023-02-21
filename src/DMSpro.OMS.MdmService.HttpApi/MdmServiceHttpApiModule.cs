using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using DMSpro.OMS.MdmService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;

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
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(ItemImageCreateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(ItemImageUpdateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CustomerAttachmentCreateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(CustomerAttachmentUpdateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(EmployeeAttachmentCreateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(EmployeeAttachmentUpdateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(EmployeeImageCreateDto));
            options.ConventionalControllers.FormBodyBindingIgnoredTypes.Add(typeof(EmployeeImageUpdateDto));
        });
    }
}
