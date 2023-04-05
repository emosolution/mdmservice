using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupLists;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupListRepository _customerGroupListRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        public CustomerGroupListsDataSeedContributor(ICustomerGroupListRepository customerGroupListRepository, IUnitOfWorkManager unitOfWorkManager, CustomersDataSeedContributor customersDataSeedContributor, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor)
        {
            _customerGroupListRepository = customerGroupListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor; _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customersDataSeedContributor.SeedAsync(context);
            await _customerGroupsDataSeedContributor.SeedAsync(context);

            await _customerGroupListRepository.InsertAsync(new CustomerGroupList
            (
                id: Guid.Parse("8db9ae5a-ff2e-4197-a667-6e6ce2be499f"),
                description: "32de6dc4b5a3461fab05",
                active: true,
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _customerGroupListRepository.InsertAsync(new CustomerGroupList
            (
                id: Guid.Parse("bd122a43-609a-4d91-ba43-b74af4e22391"),
                description: "c38a3c86e33e4bbdafc0",
                active: true,
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}