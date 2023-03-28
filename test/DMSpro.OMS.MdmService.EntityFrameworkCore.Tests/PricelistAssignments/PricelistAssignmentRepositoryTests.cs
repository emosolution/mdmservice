using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;

        public PricelistAssignmentRepositoryTests()
        {
            _pricelistAssignmentRepository = GetRequiredService<IPricelistAssignmentRepository>();
        }
    }
}