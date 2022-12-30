using Volo.Abp.Modularity;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(MdmServiceApplicationModule),
    typeof(MdmServiceDomainTestModule)
    )]
public class MdmServiceApplicationTestModule : AbpModule
{

}
