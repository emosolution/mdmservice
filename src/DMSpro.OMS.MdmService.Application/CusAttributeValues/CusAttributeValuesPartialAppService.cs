using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.CustomerAttributes;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    [Authorize(MdmServicePermissions.CusAttributeValues.Default)]
    public partial class CusAttributeValuesAppService : PartialAppService<CusAttributeValue, CusAttributeValueWithDetailsDto, ICusAttributeValueRepository>,
        ICusAttributeValuesAppService
    {
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;
        private readonly IDistributedCache<CusAttributeValueExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CusAttributeValueManager _cusAttributeValueManager;

        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CusAttributeValuesAppService(ICurrentTenant currentTenant,
            ICusAttributeValueRepository repository,
            CusAttributeValueManager cusAttributeValueManager,
            IConfiguration settingProvider,
            ICustomerAttributeRepository customerAttributeRepository,
            IDistributedCache<CusAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CusAttributeValues.Default)
        {
            _cusAttributeValueRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _cusAttributeValueManager = cusAttributeValueManager;

            _customerAttributeRepository = customerAttributeRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICusAttributeValueRepository", _cusAttributeValueRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttributeRepository", _customerAttributeRepository));
        }
    }
}