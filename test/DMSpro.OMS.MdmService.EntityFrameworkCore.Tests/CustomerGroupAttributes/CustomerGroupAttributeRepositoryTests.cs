using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;

        public CustomerGroupAttributeRepositoryTests()
        {
            _customerGroupAttributeRepository = GetRequiredService<ICustomerGroupAttributeRepository>();
        }
    }
}