using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(MdmServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class MdmServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(MdmServiceApplicationContractsModule).Assembly,
            MdmServiceRemoteServiceConsts.RemoteServiceName);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MdmServiceHttpApiClientModule>();
        });
    }
}
