using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(MdmServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class MdmServiceApplicationContractsModule : AbpModule
{

}
