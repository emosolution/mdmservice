using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValueRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;

        public CustomerAttributeValueRepositoryTests()
        {
            _customerAttributeValueRepository = GetRequiredService<ICustomerAttributeValueRepository>();
        }
    }
}