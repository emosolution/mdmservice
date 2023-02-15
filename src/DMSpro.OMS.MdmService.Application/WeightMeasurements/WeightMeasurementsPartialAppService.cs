using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
	[Authorize(MdmServicePermissions.WeightMeasurements.Default)]
	public partial class WeightMeasurementsAppService : PartialAppService<WeightMeasurement, WeightMeasurementDto, IWeightMeasurementRepository>,
		IWeightMeasurementsAppService
	{
		private readonly IWeightMeasurementRepository _weightMeasurementRepository;
		private readonly IDistributedCache<WeightMeasurementExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly WeightMeasurementManager _weightMeasurementManager;

		public WeightMeasurementsAppService(ICurrentTenant currentTenant,
			IWeightMeasurementRepository repository,
			WeightMeasurementManager weightMeasurementManager,
			IConfiguration settingProvider,
			IDistributedCache<WeightMeasurementExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_weightMeasurementRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_weightMeasurementManager = weightMeasurementManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IWeightMeasurementRepository", _weightMeasurementRepository));
		}
    }
}