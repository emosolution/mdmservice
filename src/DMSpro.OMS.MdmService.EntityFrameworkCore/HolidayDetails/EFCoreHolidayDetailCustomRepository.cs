using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class EFCoreHolidayDetailCustomRepository : EfCoreRepository<MdmServiceDbContext, HolidayDetail, Guid>, IHolidayDetailCustomRepository
    {
        public EFCoreHolidayDetailCustomRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<HolidayDetail>> GetHolidayDetailsWithinRange(DateTime dateStart, DateTime dateEnd, CancellationToken cancellationToken = default)
        {
            DateTime dateEndMax = dateEnd.Date.AddDays(1).AddSeconds(-1);
            return (await GetDbSetAsync()).Where(b => b.StartDate.Date >= dateStart.Date || b.EndDate.Date <= dateEndMax).ToList();
        }
    }
}