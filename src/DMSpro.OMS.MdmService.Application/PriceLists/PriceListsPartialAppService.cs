using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Vendors;

namespace DMSpro.OMS.MdmService.PriceLists
{
	[Authorize(MdmServicePermissions.PriceLists.Default)]
	public partial class PriceListsAppService : PartialAppService<PriceList, PriceListWithDetailsDto, IPriceListRepository>,
		IPriceListsAppService
	{
		private readonly IPriceListRepository _priceListRepository;
		private readonly PriceListManager _priceListManager;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IItemRepository _itemRepository;
        public PriceListsAppService(ICurrentTenant currentTenant,
			IPriceListRepository repository,
			PriceListManager priceListManager,
			IConfiguration settingProvider,
			IPriceListDetailRepository priceListDetailRepository,
			ICustomerRepository customerRepository,
			IVendorRepository vendorRepository,
			IItemRepository itemRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.PriceLists.Default)
		{
			_priceListRepository = repository;
			_priceListManager = priceListManager;
			_priceListDetailRepository = priceListDetailRepository;
            _customerRepository = customerRepository;
            _vendorRepository = vendorRepository;
            _itemRepository = itemRepository;
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IPriceListRepository", _priceListRepository));
		}
    }
}