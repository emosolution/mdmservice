using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupByLists;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupByListRepository _customerGroupByListRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerGroupByListsDataSeedContributor(ICustomerGroupByListRepository customerGroupByListRepository, IUnitOfWorkManager unitOfWorkManager,  
            CustomersDataSeedContributor customersDataSeedContributor,
            CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor)
        {
            _customerGroupByListRepository = customerGroupByListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor; 
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);

            await _customerGroupByListRepository.InsertAsync(new CustomerGroupByList
            (
                id: Guid.Parse("6fc722f7-d5f6-45f2-8166-dbcaebb578c3"),
                active: true,
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _customerGroupByListRepository.InsertAsync(new CustomerGroupByList
            (
                id: Guid.Parse("a11e04cd-8619-4bea-8f5f-27608f47e0da"),
                active: true,
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}