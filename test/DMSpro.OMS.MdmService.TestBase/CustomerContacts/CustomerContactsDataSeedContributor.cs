using DMSpro.OMS.MdmService.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerContactRepository _customerContactRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerContactsDataSeedContributor(ICustomerContactRepository customerContactRepository, 
            CustomersDataSeedContributor customersDataSeedContributor,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _customerContactRepository = customerContactRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customersDataSeedContributor.SeedAsync(context);

            await _customerContactRepository.InsertAsync(new CustomerContact
            (
                id: Guid.Parse("5deb6549-b60f-4dc7-a3b8-b62ec7e734b4"),
                title: default,
                firstName: "e81317183dbf4eb997fd3ad9c7e8b6c5",
                lastName: "8be501c06f",
                gender: default,
                dateOfBirth: new DateTime(2002, 6, 4),
                phone: "914f8da861b649f3b781783ace6f39e0b9c3d62972894fb7b215d7139b5f",
                email: "e559e05604964fb0a8fdfcbb7245112bbedbb2dadc014d98b35be60b",
                address: "c9fbeb658c4446d794710c3d6bec38cb435f6d0e2d134c4181582484daf6f88aad71b7457e1d41f",
                identityNumber: "11dec0cd42db4f07975aa2c08a868094ccce9555f20047ee98e5232ee5e3b8465bcb4c6bfdc64020946d9ab3",
                bankName: "af0ef61eaece4e5ca19ae6f65f1241d75ab879a3201c4fc9af7b10133cebdf5736802cc22e6046919850ebbf6671a5a",
                bankAccName: "855ca411b56444dfbd1ebdea33b9e0b322656c1bc8134eeaaf743b9c6",
                bankAccNumber: "f9bd3472d50342d99097988b67597a89f0b6",
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _customerContactRepository.InsertAsync(new CustomerContact
            (
                id: Guid.Parse("f74433b5-1678-44e5-8cfc-e89661efc363"),
                title: default,
                firstName: "27b3ad2e5a9a46ed9bbb788a9208d",
                lastName: "acc2be4a16384ba9bef0a3f59a90a1fcbb28e62ab1f0498a80055",
                gender: default,
                dateOfBirth: new DateTime(2014, 9, 10),
                phone: "79d955bfaea84b03a571bc31008fc23234b5582a599a4e599f029dfb88438520caa5ad13fcbf",
                email: "6b9ced41b1b9456abd8b1180b6f18c9191256e6a665e48cebfef11f05f49e945089a2a63811941108bad387811a2c",
                address: "2ecbda9e8089425ca6479a7eaf9c219a4",
                identityNumber: "906e653b74e34609aa246da6b186c61ca769e939d6",
                bankName: "10a65399427847aeb0ee3cf14f84a8358c5166c11e8045faae5f",
                bankAccName: "c273c155e7b74c1ca0",
                bankAccNumber: "9099ab7b5c034f5f8a23c28e2dec97275b39a0af42bf47e0a11c08",
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}