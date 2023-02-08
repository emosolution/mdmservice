using System.Collections.Generic;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial class GeoMastersAppService : PartialAppService<GeoMaster, GeoMasterDto, IGeoMasterRepository>, 
        IGeoMastersAppService
    {
        private readonly IDistributedCache<GeoMasterExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IGeoMasterRepository _geoMasterRepository;
        private readonly GeoMasterManager _geoMasterManager;

        public GeoMastersAppService(ICurrentTenant currentTenant,
            IGeoMasterRepository repository,
            GeoMasterManager geoMasterManager,
            IConfiguration settingProvider,
            IDistributedCache<GeoMasterExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _geoMasterRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _geoMasterManager = geoMasterManager;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
        }
    }
}
