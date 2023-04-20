using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public class SalesOrgEmpAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;

        public SalesOrgEmpAssignmentRepositoryTests()
        {
            _salesOrgEmpAssignmentRepository = GetRequiredService<ISalesOrgEmpAssignmentRepository>();
        }
    }
}