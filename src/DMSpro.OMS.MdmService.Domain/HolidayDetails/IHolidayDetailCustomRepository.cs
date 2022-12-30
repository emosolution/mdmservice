using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public interface IHolidayDetailCustomRepository : IRepository<HolidayDetail, Guid>
    {
        Task<List<HolidayDetail>> GetHolidayDetailsWithinRange(DateTime dateStart, DateTime dateEnd,
            CancellationToken cancellationToken = default);
    }
}