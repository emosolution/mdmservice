using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigsAppService : PartialAppService<NumberingConfig, NumberingConfigWithDetailsDto, INumberingConfigRepository>,
        INumberingConfigsAppService
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        private readonly ISystemDataRepository _systemDataRepository;

        public NumberingConfigsAppService(ICurrentTenant currentTenant,
            INumberingConfigRepository repository,
            IConfiguration settingProvider,
            ISystemDataRepository systemDataRepository,
            INumberingConfigDetailRepository numberingConfigDetailRepository)
            : base(currentTenant, repository, settingProvider)
        {
            _numberingConfigRepository = repository;

            _systemDataRepository = systemDataRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigRepository", _numberingConfigRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
        }
    }
}