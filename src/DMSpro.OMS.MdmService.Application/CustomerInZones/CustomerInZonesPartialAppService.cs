using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Customers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.CustomerInZones
{
    [Authorize(MdmServicePermissions.CustomerInZones.Default)]
    public partial class CustomerInZonesAppService : PartialAppService<CustomerInZone, CustomerInZoneWithDetailsDto, ICustomerInZoneRepository>,
        ICustomerInZonesAppService
    {
        private readonly ICustomerInZoneRepository _customerInZoneRepository;
        private readonly IDistributedCache<CustomerInZoneExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CustomerInZoneManager _customerInZoneManager;

        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly ICustomerRepository _customerRepository;


        public CustomerInZonesAppService(ICurrentTenant currentTenant,
            ICustomerInZoneRepository repository,
            CustomerInZoneManager customerInZoneManager,
            IConfiguration settingProvider,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            ICustomerRepository customerRepository,
            IDistributedCache<CustomerInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _customerInZoneRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerInZoneManager = customerInZoneManager;

            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _customerRepository = customerRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerInZoneRepository", _customerInZoneRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
		
    }
}