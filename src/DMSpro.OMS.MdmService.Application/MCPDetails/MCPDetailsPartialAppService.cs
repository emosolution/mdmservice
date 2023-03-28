using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.MCPDetails
{
	[Authorize(MdmServicePermissions.MCPs.Default)]
	public partial class MCPDetailsAppService : PartialAppService<MCPDetail, MCPDetailWithDetailsDto, IMCPDetailRepository>,
		IMCPDetailsAppService
	{
		private readonly IMCPDetailRepository _mCPDetailRepository;
		private readonly IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly MCPDetailManager _mCPDetailManager;

		private readonly IMCPHeaderRepository _mCPHeaderRepository;
		private readonly ICustomerRepository _customerRepository;

		public MCPDetailsAppService(ICurrentTenant currentTenant,
			IMCPDetailRepository repository,
			MCPDetailManager mCPDetailManager,
			IConfiguration settingProvider,
			IMCPHeaderRepository mCPHeaderRepository,
			ICustomerRepository customerRepository,
			IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.MCPs.Default)
		{
			_mCPDetailRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_mCPDetailManager = mCPDetailManager;
			
			_mCPHeaderRepository = mCPHeaderRepository;
			_customerRepository = customerRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IMCPDetailRepository", _mCPDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IMCPHeaderRepository", _mCPHeaderRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
    }
}