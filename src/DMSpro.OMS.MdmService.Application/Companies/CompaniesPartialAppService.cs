using DMSpro.OMS.MdmService.GeoMasters;
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using Org.BouncyCastle.Asn1.Tsp;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Companies
{
    [Authorize(MdmServicePermissions.CompanyMasters.Default)]
    public partial class CompaniesAppService : PartialsAppService<Company, CompanyDto, ICompanyRepository>, ICompaniesAppService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly CompanyManager _companyManager;
        private readonly IGeoMasterRepository _geoMasterRepository;

        public CompaniesAppService(ICompanyRepository repository,
            CompanyManager companyManager,
            IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache,
            IGeoMasterRepository geoMasterRepository)
            : base(repository)
        {
            _companyRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyManager = companyManager;
            _geoMasterRepository = geoMasterRepository;
        }

        protected override async Task<Guid?> GetIdByCodeForImport(string propertyName, string code)
        {
            if (propertyName.Contains("GeoLevel"))
            {
                return await _geoMasterRepository.GetIdByCodeAsync(code);
            }
            throw new BusinessException(message: $"Encounter unknown propery {propertyName} when importing", code: "1");
        }
    }
}
