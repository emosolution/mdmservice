using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.FileManagementInfo;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomerAttachmentsAppService : PartialAppService<
    CustomerAttachment, CustomerAttachmentWithDetailsDto, ICustomerAttachmentRepository>,
        ICustomerAttachmentsAppService
    {
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;
        private readonly IDistributedCache<CustomerAttachmentExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CustomerAttachmentManager _customerAttachmentManager;
        private readonly IFileManagementInfoAppService _fileManagementInfoAppService;

        private readonly ICustomerRepository _customerRepository;

        public CustomerAttachmentsAppService(ICurrentTenant currentTenant,
            ICustomerAttachmentRepository repository,
            CustomerAttachmentManager customerAttachmentManager,
            IFileManagementInfoAppService fileManagementInfoAppService,
            IConfiguration settingProvider,
            ICustomerRepository customerRepository,
            IDistributedCache<CustomerAttachmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Customers.Default)
        {
            _customerAttachmentRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerAttachmentManager = customerAttachmentManager;
            _fileManagementInfoAppService = fileManagementInfoAppService;

            _customerRepository = customerRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAttachmentRepository", _customerAttachmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
    }
}