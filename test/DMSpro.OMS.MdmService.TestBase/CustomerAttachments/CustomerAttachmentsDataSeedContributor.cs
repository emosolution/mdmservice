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

        public CustomerAttachmentsDataSeedContributor(ICustomerAttachmentRepository customerAttachmentRepository, IUnitOfWorkManager unitOfWorkManager, CustomersDataSeedContributor customersDataSeedContributor)
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
                id: Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"),
                description: "9d11035",
                active: true,
                fileId: Guid.Parse("d66c8ccb-7637-469b-84c7-140847a4783a"),
                customerId: Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be")
            ));

            await _customerAttachmentRepository.InsertAsync(new CustomerAttachment
            (
                id: Guid.Parse("3ed21074-9c0e-46b3-83ce-c05e55dc6fd6"),
                description: "7299a06334fa",
                active: true,
                fileId: Guid.Parse("9addaadf-bd64-4374-a804-8f39fced702e"),
                customerId: Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}