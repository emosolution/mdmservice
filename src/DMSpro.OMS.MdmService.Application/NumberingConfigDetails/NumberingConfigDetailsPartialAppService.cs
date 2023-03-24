using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.SystemDatas;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigDetailsAppService :
        PartialAppService<NumberingConfigDetail, NumberingConfigDetailWithDetailsDto, INumberingConfigDetailRepository>,
        INumberingConfigDetailsAppService
    {
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;

        private readonly ICompanyRepository _companyRepository;
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly ISystemDatasInternalAppService _systemDatasInternalAppService;

        private readonly NumberingConfigDetailManager _numberingConfigDetailManager;

        public NumberingConfigDetailsAppService(ICurrentTenant currentTenant,
            INumberingConfigDetailRepository repository,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            INumberingConfigRepository numberingConfigRepository,
            ISystemDataRepository systemDataRepository,
            ISystemDatasInternalAppService systemDatasInternalAppService,
            NumberingConfigDetailManager numberingConfigDetailManager)
            : base(currentTenant, repository, settingProvider)
        {
            _numberingConfigDetailRepository = repository;
            _numberingConfigDetailManager = numberingConfigDetailManager;

            _companyRepository = companyRepository;
            _numberingConfigRepository = numberingConfigRepository;
            _systemDatasInternalAppService = systemDatasInternalAppService;
            _systemDataRepository = systemDataRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigDetailRepository", _numberingConfigDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigRepository", _numberingConfigRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
            _systemDatasInternalAppService = systemDatasInternalAppService;
        }
    }
}