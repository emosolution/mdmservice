using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IHolidayDetailRepository _holidayDetailRepository;

        public HolidayDetailRepositoryTests()
        {
            _holidayDetailRepository = GetRequiredService<IHolidayDetailRepository>();
        }
    }
}