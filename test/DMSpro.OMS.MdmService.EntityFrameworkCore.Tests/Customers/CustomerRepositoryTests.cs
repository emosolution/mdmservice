using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            _customerRepository = GetRequiredService<ICustomerRepository>();
        }
    }
}