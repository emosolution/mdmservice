using DMSpro.OMS.MdmService.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerAttachments;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerAttachmentsDataSeedContributor(ICustomerAttachmentRepository customerAttachmentRepository, 
            CustomersDataSeedContributor customersDataSeedContributor,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _customerAttachmentRepository = customerAttachmentRepository;
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

            await _customerAttachmentRepository.InsertAsync(new CustomerAttachment
            (
                id: Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"),
                url: "4b8ea87c9d204e29a98e9c10c",
                description: "96fad43a6e6742c2ae70b2a267437064513233aeb8ad4eaab1e760fd7b5e543380940",
                active: true,
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _customerAttachmentRepository.InsertAsync(new CustomerAttachment
            (
                id: Guid.Parse("f65f3c2d-f9b1-4814-a236-b7989fc07ced"),
                url: "9b1cb478e430434eb06d671136142c26b4a4ded20e384f47a9e7a249886e457f9356a9b3fdd240b3a89bf03",
                description: "afe6c7616781466aad9a8bb977240f0b01e64c5ecd70412497687c5351fe752043543b285b9b42",
                active: true,
                customerId: Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}