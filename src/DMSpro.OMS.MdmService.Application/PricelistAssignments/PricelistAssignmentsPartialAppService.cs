using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	[Authorize(MdmServicePermissions.PriceListAssignments.Default)]
	public partial class PricelistAssignmentsAppService : PartialAppService<PricelistAssignment, PricelistAssignmentWithDetailsDto, IPricelistAssignmentRepository>,
		IPricelistAssignmentsAppService
	{
		private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;
		private readonly IDistributedCache<PricelistAssignmentExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly PricelistAssignmentManager _pricelistAssignmentManager;

		private readonly IPriceListRepository _priceListRepository;
		private readonly ICustomerGroupRepository _customerGroupRepository;

		public PricelistAssignmentsAppService(ICurrentTenant currentTenant,
			IPricelistAssignmentRepository repository,
			PricelistAssignmentManager pricelistAssignmentManager,
			IConfiguration settingProvider,
			IPriceListRepository priceListRepository,
			ICustomerGroupRepository customerGroupRepository,
			IDistributedCache<PricelistAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_pricelistAssignmentRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_pricelistAssignmentManager = pricelistAssignmentManager;
			
			_priceListRepository = priceListRepository;
			_customerGroupRepository = customerGroupRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPricelistAssignmentRepository", _pricelistAssignmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("ICustomerGroupRepository", _customerGroupRepository));
        }
    }
}