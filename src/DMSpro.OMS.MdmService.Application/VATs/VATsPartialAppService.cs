using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.VATs
{
	[Authorize(MdmServicePermissions.VATs.Default)]
	public partial class VATsAppService : PartialAppService<VAT, VATDto, IVATRepository>,
		IVATsAppService
	{
		private readonly IVATRepository _vATRepository;
		private readonly IDistributedCache<VATExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly VATManager _vATManager;

		public VATsAppService(ICurrentTenant currentTenant,
			IVATRepository repository,
			VATManager vATManager,
			IConfiguration settingProvider,
			IDistributedCache<VATExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.VATs.Default)
		{
			_vATRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_vATManager = vATManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVATRepository", _vATRepository));
		}
    }
}