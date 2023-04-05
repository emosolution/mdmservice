using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
    public partial class CusAttributeValuesAppService : PartialAppService<CusAttributeValue, CusAttributeValueWithDetailsDto, ICusAttributeValueRepository>,
        ICusAttributeValuesAppService
    {
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDistributedCache<CusAttributeValueExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CusAttributeValueManager _cusAttributeValueManager;

        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CusAttributeValuesAppService(ICurrentTenant currentTenant,
            ICusAttributeValueRepository repository,
            ICustomerRepository customerRepository,
            CusAttributeValueManager cusAttributeValueManager,
            IConfiguration settingProvider,
            ICustomerAttributeRepository customerAttributeRepository,
            IDistributedCache<CusAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerAttributes.Default)
        {
            _cusAttributeValueRepository = repository;
            _customerRepository = customerRepository;
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