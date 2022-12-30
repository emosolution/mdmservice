using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public class CustomerAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAssignmentRepository _customerAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerAssignmentsDataSeedContributor(ICustomerAssignmentRepository customerAssignmentRepository, IUnitOfWorkManager unitOfWorkManager,
            CustomersDataSeedContributor customersDataSeedContributor,
            CompaniesDataSeedContributor companiesDataSeedContributor
        )
        {
            _customerAssignmentRepository = customerAssignmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor; 
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);

            await _customerAssignmentRepository.InsertAsync(new CustomerAssignment
            (
                id: Guid.Parse("a016e959-ee1f-4ec3-9f99-eb73c5b0852c"),
                effectiveDate: new DateTime(2008, 8, 2),
                endDate: new DateTime(2005, 8, 12),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _customerAssignmentRepository.InsertAsync(new CustomerAssignment
            (
                id: Guid.Parse("e570c911-ac6d-48ba-9695-6e9d2683c3b8"),
                effectiveDate: new DateTime(2021, 6, 6),
                endDate: new DateTime(2012, 9, 6),
                companyId: Guid.Parse("97c129fa-c970-43b3-9230-6e1353c77557"),
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}