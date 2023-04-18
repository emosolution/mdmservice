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
        public virtual Task<CustomerProfileDto> GetCustomerProfileAsync(Guid id)
        {
            return _customersAppService.GetCustomerProfileAsync(id);
        }
    }
}
