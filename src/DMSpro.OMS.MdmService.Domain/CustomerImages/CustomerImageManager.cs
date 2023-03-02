using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageManager : DomainService
    {
        private readonly ICustomerImageRepository _customerImageRepository;

        public CustomerImageManager(ICustomerImageRepository customerImageRepository)
        {
            _customerImageRepository = customerImageRepository;
        }

        public async Task<CustomerImage> CreateAsync(
        Guid customerId, string description, bool active, bool isAvatar, bool isPOSM, Guid fileId)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(description, nameof(description), CustomerImageConsts.DescriptionMaxLength);

            var customerImage = new CustomerImage(
             GuidGenerator.Create(),
             customerId, description, active, isAvatar, isPOSM, fileId
             );

            return await _customerImageRepository.InsertAsync(customerImage);
        }

        public async Task<CustomerImage> UpdateAsync(
            Guid id,
            Guid customerId, string description, bool active, bool isAvatar, bool isPOSM, Guid fileId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.Length(description, nameof(description), CustomerImageConsts.DescriptionMaxLength);

            var customerImage = await _customerImageRepository.GetAsync(id);

            customerImage.CustomerId = customerId;
            customerImage.Description = description;
            customerImage.Active = active;
            customerImage.IsAvatar = isAvatar;
            customerImage.IsPOSM = isPOSM;
            customerImage.FileId = fileId;

            customerImage.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerImageRepository.UpdateAsync(customerImage);
        }

    }
}