using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
	[Authorize(MdmServicePermissions.CustomerAssignments.Default)]
	public partial class CustomerAssignmentsAppService : PartialAppService<CustomerAssignment, CustomerAssignmentWithDetailsDto, ICustomerAssignmentRepository>,
		ICustomerAssignmentsAppService
	{
		private readonly ICustomerAssignmentRepository _customerAssignmentRepository;
		private readonly IDistributedCache<CustomerAssignmentExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly CustomerAssignmentManager _customerAssignmentManager;

		private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;

		public CustomerAssignmentsAppService(ICurrentTenant currentTenant,
			ICustomerAssignmentRepository repository,
			CustomerAssignmentManager customerAssignmentManager,
			IConfiguration settingProvider,
			ICompanyRepository companyRepository,
			ICustomerRepository customerRepository,
			IDistributedCache<CustomerAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.CustomerAssignments.Default)
		{
			_customerAssignmentRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_customerAssignmentManager = customerAssignmentManager;
			
			_companyRepository = companyRepository;
			_customerRepository = customerRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerAssignmentRepository", _customerAssignmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
            _repositories.AddIfNotContains(
                    new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
    }
}