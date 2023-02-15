using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.MCPHeaders;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    [Authorize(MdmServicePermissions.MCPDetails.Default)]
    public partial class MCPDetailsAppService : PartialAppService<MCPDetail, MCPDetailDto, IMCPDetailRepository>, IMCPDetailsAppService
    {
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly MCPDetailManager _mCPDetailManager;

        private readonly ICustomerRepository _customerRepository;
        private readonly IMCPHeaderRepository _mCPHeaderRepository;

        public MCPDetailsAppService(ICurrentTenant currentTenant,
            IMCPDetailRepository repository,
            MCPDetailManager companyManager,
            IConfiguration settingProvider,
            ICustomerRepository customerRepository,
            IMCPHeaderRepository mCPHeaderRepository,
            IDistributedCache<MCPDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _mCPDetailRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mCPDetailManager = companyManager;

            _customerRepository = customerRepository;
            _mCPHeaderRepository = mCPHeaderRepository;
            _repositories.Add("ICustomerRepository", _customerRepository);
            _repositories.Add("IMCPHeaderRepository", _mCPHeaderRepository);
            _repositories.Add("IMCPDetailRepository", _mCPDetailRepository);
        }
    }
}