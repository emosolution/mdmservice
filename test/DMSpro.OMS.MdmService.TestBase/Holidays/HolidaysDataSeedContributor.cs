using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Holidays;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidaysDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public HolidaysDataSeedContributor(IHolidayRepository holidayRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _holidayRepository = holidayRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _holidayRepository.InsertAsync(new Holiday
            (
                id: Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756"),
                year: 2084,
                description: "628145de1e984f499"
            ));

            await _holidayRepository.InsertAsync(new Holiday
            (
                id: Guid.Parse("a874421a-b7c7-4e6f-ba3e-7af161ab5e8e"),
                year: 2065,
                description: "486069a0ab8a4979a526a8d6b924fdfdd34ff825a"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}