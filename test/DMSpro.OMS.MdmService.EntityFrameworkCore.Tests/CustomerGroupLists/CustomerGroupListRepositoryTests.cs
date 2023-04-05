using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupListRepository _customerGroupListRepository;

        public CustomerGroupListRepositoryTests()
        {
            _customerGroupListRepository = GetRequiredService<ICustomerGroupListRepository>();
        }
    }
}