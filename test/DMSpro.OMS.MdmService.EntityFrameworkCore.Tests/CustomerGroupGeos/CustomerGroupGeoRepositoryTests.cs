using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerGroupGeoRepository _customerGroupGeoRepository;

        public CustomerGroupGeoRepositoryTests()
        {
            _customerGroupGeoRepository = GetRequiredService<ICustomerGroupGeoRepository>();
        }
    }
}