using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyRepositoryTests()
        {
            _salesOrgHierarchyRepository = GetRequiredService<ISalesOrgHierarchyRepository>();
        }
    }
}