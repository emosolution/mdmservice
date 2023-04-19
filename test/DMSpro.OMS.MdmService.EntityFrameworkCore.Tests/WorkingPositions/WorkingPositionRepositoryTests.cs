using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IWorkingPositionRepository _workingPositionRepository;

        public WorkingPositionRepositoryTests()
        {
            _workingPositionRepository = GetRequiredService<IWorkingPositionRepository>();
        }
    }
}