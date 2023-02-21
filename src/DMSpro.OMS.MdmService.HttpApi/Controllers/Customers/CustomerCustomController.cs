using DMSpro.OMS.MdmService.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.Customers
{
    public partial class CustomerController
    {
        [HttpGet]
        [Route("customer-profile/{id}")]
        public async Task<CustomerProfileDto> GetCustomerProfile(Guid id)
        {
            return await _customersAppService.GetCustomerProfile(id);
        }
    }
}
