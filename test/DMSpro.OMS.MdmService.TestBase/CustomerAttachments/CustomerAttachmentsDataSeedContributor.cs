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
                id: Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da"),
                description: "e9302fdea6254157a163a71c7f61fd3bbae5b3558a1f4bf29168ad3494598cc0787b9c214b49420fba0ab9edf8f65ca6067c10cd22e24faa9b5c5a885bd3906457167b3b5fbc4b7395cbf8fc91e0146d78b87f37511f4d5d9d70ba50adb70064aac9be05ef3345f3aa30221a47a79ebe39c7b754301d4bcd8794edfc241131d3353c514aa42347b2b4afb42b5be26af408da05b773d84872a7ce6c2805153701221196944158445688899e9ea024ed97e1a24b8297d449d98c361fecdb2248abe61bd5dad40f4974ab7a9b0a9bff65d824c16aadd53640a9b9ef3c3d72d9e45cc60af7b2bcbb435d9621bffd73e41cf8778ed2c4d453461190d2",
                active: true,
                fileId: Guid.Parse("dadc7f8e-a997-43dc-b16f-920135fa87fe"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _customerAttachmentRepository.InsertAsync(new CustomerAttachment
            (
                id: Guid.Parse("9d413f20-2063-4257-83b7-e59e6566c43f"),
                description: "4f75e365e8b24947a8e742eb6680c040ec5e1698d7d942748bb270d44c0f39ebd1c68f6a8d41496fad16ef614749fd00d98f0c58e80a439c9d592a5e252782b3d222d1037b3a48f59e0538c9bd2896c897c0301aff1844f8984332905cf8706e6daa6f3d2ca8450989e583026d8750caff32a9ac4b93424b916f53f742500bfdb561a8d01119451a94b8d6657f7c2ff1a8a1911cc436450886d737b3cf6f88babd34ad6b20e746bfb4540431aefd546ce6c2c99de28b4fafbfe04eb302dc2b5a9564e54d01124b25913ba6a68034f0f88004bc42b9014a1c81fa26fadf012c3d78d5772b52dd420e8ac276210b334e66dcfa34b4f98a47fb9d6a",
                active: true,
                fileId: Guid.Parse("9d0011ee-6fb7-41c1-9797-8da2df2eabbe"),
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}