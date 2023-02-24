using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Customers
{
    public partial interface ICustomersAppService
    {
        Task<CustomerProfileDto> GetCustomerProfileAsync(Guid id);
    }
}
