using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.Vendors
{
    [Authorize(MdmServicePermissions.Vendors.Default)]
    public partial class VendorsAppService : PartialAppService<Vendor, VendorWithDetailsDto, IVendorRepository>,
        IVendorsAppService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly VendorManager _vendorManager;
        private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;

        private readonly IPriceListRepository _priceListRepository;
        private readonly IGeoMasterRepository _geoMasterRepository;
        private readonly ICompanyRepository _companyRepository;

        public VendorsAppService(ICurrentTenant currentTenant,
            IVendorRepository repository,
            VendorManager vendorManager,
            INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
            IConfiguration settingProvider,
            IPriceListRepository priceListRepository,
            IGeoMasterRepository geoMasterRepository,
            ICompanyRepository companyRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Vendors.Default)
        {
            _vendorRepository = repository;
            _vendorManager = vendorManager;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;

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