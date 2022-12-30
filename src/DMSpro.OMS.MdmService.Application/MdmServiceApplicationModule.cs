using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Modularity;
using Volo.Abp.BackgroundWorkers;
using Microsoft.Extensions.Configuration;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(MdmServiceDomainModule),
    typeof(MdmServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpBackgroundWorkersHangfireModule))]
public class MdmServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAutoMapperObjectMapper<MdmServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MdmServiceApplicationModule>(validate: true);
        });
        ConfigureHangfire(context, configuration);
    }

    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<IHangfireScheduledVisitPlanGeneration>();
    }

    private static void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("MdmService"));
        });
    }

}
