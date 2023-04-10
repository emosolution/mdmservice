using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorRepositoryTests()
        {
            _vendorRepository = GetRequiredService<IVendorRepository>();
        }
    }
}