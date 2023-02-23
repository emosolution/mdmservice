using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.FileManagementInfo;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
	[Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
	public partial class EmployeeImagesAppService : PartialAppService<EmployeeImage, EmployeeImageWithDetailsDto, IEmployeeImageRepository>,
		IEmployeeImagesAppService
	{
		private readonly IEmployeeImageRepository _employeeImageRepository;
		private readonly IDistributedCache<EmployeeImageExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly EmployeeImageManager _employeeImageManager;
        private readonly IFileManagementInfoAppService _fileManagementInfoAppService;

        private readonly IEmployeeProfileRepository _employeeProfileRepository;

		public EmployeeImagesAppService(ICurrentTenant currentTenant,
			IEmployeeImageRepository repository,
			EmployeeImageManager employeeImageManager,
			IFileManagementInfoAppService fileManagementInfoAppService,
			IConfiguration settingProvider,
			IEmployeeProfileRepository employeeProfileRepository,
			IDistributedCache<EmployeeImageExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_employeeImageRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_employeeImageManager = employeeImageManager;
            _fileManagementInfoAppService = fileManagementInfoAppService;


            _employeeProfileRepository = employeeProfileRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeImageRepository", _employeeImageRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
        }
    }
}