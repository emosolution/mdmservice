using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAttributeRepository _customerAttributeRepository;

        public CustomerAttributeRepositoryTests()
        {
            _customerAttributeRepository = GetRequiredService<ICustomerAttributeRepository>();
        }
    }
}