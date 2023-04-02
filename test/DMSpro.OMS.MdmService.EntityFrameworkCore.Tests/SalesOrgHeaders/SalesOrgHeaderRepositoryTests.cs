using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

        public SalesOrgHeaderRepositoryTests()
        {
            _salesOrgHeaderRepository = GetRequiredService<ISalesOrgHeaderRepository>();
        }
    }
}