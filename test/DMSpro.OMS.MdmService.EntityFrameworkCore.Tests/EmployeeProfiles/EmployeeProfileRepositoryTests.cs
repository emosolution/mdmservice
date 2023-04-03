using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IEmployeeProfileRepository _employeeProfileRepository;

        public EmployeeProfileRepositoryTests()
        {
            _employeeProfileRepository = GetRequiredService<IEmployeeProfileRepository>();
        }
    }
}