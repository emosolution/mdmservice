using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupRepository _customerGroupRepository;

        public CustomerGroupRepositoryTests()
        {
            _customerGroupRepository = GetRequiredService<ICustomerGroupRepository>();
        }
    }
}