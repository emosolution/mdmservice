using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;
using System.Runtime.CompilerServices;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.Vendors
{
    [Authorize(MdmServicePermissions.Vendors.Default)]
    public partial class VendorsAppService : PartialAppService<Vendor, VendorWithDetailsDto, IVendorRepository>,
        IVendorsAppService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDistributedCache<VendorExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly VendorManager _vendorManager;

        private readonly IPriceListRepository _priceListRepository;
        private readonly IGeoMasterRepository _geoMasterRepository;
        private readonly ICompanyRepository _companyRepository;

        public VendorsAppService(ICurrentTenant currentTenant,
            IVendorRepository repository,
            VendorManager vendorManager,
            IConfiguration settingProvider,
            IPriceListRepository priceListRepository,
            IGeoMasterRepository geoMasterRepository,
            ICompanyRepository companyRepository,
            IDistributedCache<VendorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _vendorRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _vendorManager = vendorManager;

            _priceListRepository = priceListRepository;
            _geoMasterRepository = geoMasterRepository;
            _companyRepository = companyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVendorRepository", _vendorRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}