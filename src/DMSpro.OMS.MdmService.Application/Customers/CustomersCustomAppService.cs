using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Customers
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomersAppService
    {
        public async Task<CustomerProfileDto> GetCustomerProfile(Guid id)
        {
            Customer customer = await _customerRepository.GetAsync(id);
            List<CustomerAttachment> attachments = (await _customerAttachmentRepository.GetQueryableAsync()).Where(x => x.CustomerId == id).ToList();
            var result = new CustomerProfileDto()
            {
                Customer = ObjectMapper.Map<Customer, CustomerDto>(customer),
                Attachments = ObjectMapper.Map<List<CustomerAttachment>, List<CustomerAttachmentDto>>(attachments),
            };
            return result;
        }
    }
}
