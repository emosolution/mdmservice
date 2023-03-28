using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.FileManagementInfo;
using DMSpro.OMS.MdmService.Items;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomerImagesAppService : PartialAppService<
        CustomerImage, CustomerImageWithDetailsDto, ICustomerImageRepository>,
        ICustomerImagesAppService
    {
        private readonly ICustomerImageRepository _customerImageRepository;
        private readonly IDistributedCache<CustomerImageExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly CustomerImageManager _customerImageManager;
        private readonly IFileManagementInfoAppService _fileManagementInfoAppService;

        private readonly ICustomerRepository _customerRepository;
        private readonly IItemRepository _itemRepository;

        public CustomerImagesAppService(ICurrentTenant currentTenant,
            ICustomerImageRepository repository,
            CustomerImageManager customerImageManager,
            IFileManagementInfoAppService fileManagementInfoAppService,
            IConfiguration settingProvider,
            ICustomerRepository customerRepository,
            IItemRepository itemRepository,
            IDistributedCache<CustomerImageExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.Customers.Default)
        {
            _customerImageRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerImageManager = customerImageManager;
            _fileManagementInfoAppService = fileManagementInfoAppService;

            _customerRepository = customerRepository;
            _itemRepository = itemRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerImageRepository", _customerImageRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
        }
    }
}