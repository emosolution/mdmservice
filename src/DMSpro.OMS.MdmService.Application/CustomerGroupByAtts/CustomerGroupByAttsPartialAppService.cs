using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CusAttributeValues;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    [Authorize(MdmServicePermissions.CustomerGroupByAtts.Default)]
    public partial class CustomerGroupByAttsAppService : PartialAppService<CustomerGroupByAtt, CustomerGroupByAttsWithDetailsDto, ICustomerGroupByAttRepository>,
        ICustomerGroupByAttsAppService
    {
        private readonly ICustomerGroupByAttRepository _customerGroupByAttRepository;
        private readonly IDistributedCache<CustomerGroupByAttExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CustomerGroupByAttManager _customerGroupByAttManager;

        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;

        public CustomerGroupByAttsAppService(ICurrentTenant currentTenant,
            ICustomerGroupByAttRepository repository,
            CustomerGroupByAttManager customerGroupByAttManager,
            IConfiguration settingProvider,
            ICustomerGroupRepository customerGroupRepository,
            ICusAttributeValueRepository cusAttributeValueRepository,
            IDistributedCache<CustomerGroupByAttExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerGroupByAtts.Default)
        {
            _customerGroupByAttRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerGroupByAttManager = customerGroupByAttManager;

            _customerGroupRepository = customerGroupRepository;
            _cusAttributeValueRepository = cusAttributeValueRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupByAttRepository", _customerGroupByAttRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerGroupRepository", customerGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICusAttributeValueRepository", cusAttributeValueRepository));
        }
    }
}