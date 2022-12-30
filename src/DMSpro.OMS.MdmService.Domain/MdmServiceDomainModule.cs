using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MdmServiceDomainSharedModule)
)]
public class MdmServiceDomainModule : AbpModule
{
}
