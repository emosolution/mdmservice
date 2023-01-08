using Volo.Abp.Domain;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace DMSpro.OMS.MdmService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(MdmServiceDomainSharedModule)
)]
public class MdmServiceDomainModule : AbpModule
{
}
