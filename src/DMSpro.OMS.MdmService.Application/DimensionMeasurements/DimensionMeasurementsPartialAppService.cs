using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
	[Authorize(MdmServicePermissions.DimensionMeasurements.Default)]
	public partial class DimensionMeasurementsAppService : PartialAppService<DimensionMeasurement, DimensionMeasurementDto, IDimensionMeasurementRepository>,
		IDimensionMeasurementsAppService
	{
		private readonly IDimensionMeasurementRepository _dimensionMeasurementRepository;
		private readonly IDistributedCache<DimensionMeasurementExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly DimensionMeasurementManager _dimensionMeasurementManager;

		public DimensionMeasurementsAppService(ICurrentTenant currentTenant,
			IDimensionMeasurementRepository repository,
			DimensionMeasurementManager dimensionMeasurementManager,
			IConfiguration settingProvider,
			IDistributedCache<DimensionMeasurementExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.DimensionMeasurements.Default)
		{
			_dimensionMeasurementRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_dimensionMeasurementManager = dimensionMeasurementManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IDimensionMeasurementRepository", _dimensionMeasurementRepository));
		}
    }
}