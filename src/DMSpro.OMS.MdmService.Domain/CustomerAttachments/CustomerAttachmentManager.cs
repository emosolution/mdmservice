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
        Guid customerId, string url, string description, bool active)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var customerAttachment = new CustomerAttachment(
             GuidGenerator.Create(),
             customerId, url, description, active
             );

            return await _customerAttachmentRepository.InsertAsync(customerAttachment);
        }

        public async Task<CustomerAttachment> UpdateAsync(
            Guid id,
            Guid customerId, string url, string description, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNullOrWhiteSpace(url, nameof(url));

            var queryable = await _customerAttachmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var customerAttachment = await AsyncExecuter.FirstOrDefaultAsync(query);

            customerAttachment.CustomerId = customerId;
            customerAttachment.url = url;
            customerAttachment.Description = description;
            customerAttachment.Active = active;

            customerAttachment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAttachmentRepository.UpdateAsync(customerAttachment);
        }

    }
}