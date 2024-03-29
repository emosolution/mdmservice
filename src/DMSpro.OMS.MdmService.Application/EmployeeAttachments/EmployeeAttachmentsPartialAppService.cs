using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.FileManagementInfo;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	[Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
	public partial class EmployeeAttachmentsAppService : PartialAppService<EmployeeAttachment, EmployeeAttachmentWithDetailsDto, IEmployeeAttachmentRepository>,
		IEmployeeAttachmentsAppService
	{
		private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;
		private readonly IDistributedCache<EmployeeAttachmentExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly EmployeeAttachmentManager _employeeAttachmentManager;
        private readonly IFileManagementInfoAppService _fileManagementInfoAppService;

        private readonly IEmployeeProfileRepository _employeeProfileRepository;

		public EmployeeAttachmentsAppService(ICurrentTenant currentTenant,
			IEmployeeAttachmentRepository repository,
			EmployeeAttachmentManager employeeAttachmentManager,
			IFileManagementInfoAppService fileManagementInfoAppService,
			IConfiguration settingProvider,
			IEmployeeProfileRepository employeeProfileRepository,
			IDistributedCache<EmployeeAttachmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.EmployeeProfiles.Default)
		{
			_employeeAttachmentRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_employeeAttachmentManager = employeeAttachmentManager;
			_fileManagementInfoAppService = fileManagementInfoAppService;
			
			_employeeProfileRepository = employeeProfileRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeAttachmentRepository", _employeeAttachmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
        }
    }
}