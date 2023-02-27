using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentManager : DomainService
    {
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;

        public CustomerAttachmentManager(ICustomerAttachmentRepository customerAttachmentRepository)
        {
            _customerAttachmentRepository = customerAttachmentRepository;
        }

        public async Task<CustomerAttachment> CreateAsync(
        Guid customerId, string description, bool active, Guid fileId)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(description, nameof(description), CustomerAttachmentConsts.DescriptionMaxLength);

            var customerAttachment = new CustomerAttachment(
             GuidGenerator.Create(),
             customerId, description, active, fileId
             );

            return await _customerAttachmentRepository.InsertAsync(customerAttachment);
        }

        public async Task<CustomerAttachment> UpdateAsync(
            Guid id,
            Guid customerId, string description, bool active, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(description, nameof(description), CustomerAttachmentConsts.DescriptionMaxLength);

            var customerAttachment = await _customerAttachmentRepository.GetAsync(id);

            customerAttachment.CustomerId = customerId;
            customerAttachment.Description = description;
            customerAttachment.Active = active;
            customerAttachment.FileId = fileId;

            customerAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAttachmentRepository.UpdateAsync(customerAttachment);
        }

    }
}