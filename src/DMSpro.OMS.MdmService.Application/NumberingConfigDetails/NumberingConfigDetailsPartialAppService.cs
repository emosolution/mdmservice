using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SystemDatas;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigDetailsAppService : PartialAppService<NumberingConfigDetail, NumberingConfigDetailWithDetailsDto, INumberingConfigDetailRepository>,
        INumberingConfigDetailsAppService
    {
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;
        private readonly IDistributedCache<NumberingConfigDetailExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly NumberingConfigDetailManager _numberingConfigDetailManager;

        private readonly ICompanyRepository _companyRepository;
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigDetailsAppService(ICurrentTenant currentTenant,
            INumberingConfigDetailRepository repository,
            NumberingConfigDetailManager numberingConfigDetailManager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            INumberingConfigRepository numberingConfigRepository,
            IDistributedCache<NumberingConfigDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _numberingConfigDetailRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _numberingConfigDetailManager = numberingConfigDetailManager;

            _companyRepository = companyRepository;
            _numberingConfigRepository = numberingConfigRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigDetailRepository", _numberingConfigDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("INumberingConfigRepository", _numberingConfigRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}