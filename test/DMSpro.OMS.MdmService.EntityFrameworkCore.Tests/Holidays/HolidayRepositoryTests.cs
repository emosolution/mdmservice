using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayRepositoryTests()
        {
            _holidayRepository = GetRequiredService<IHolidayRepository>();
        }
    }
}