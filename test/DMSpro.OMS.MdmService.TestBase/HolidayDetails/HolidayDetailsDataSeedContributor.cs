using DMSpro.OMS.MdmService.Holidays;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.HolidayDetails;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IHolidayDetailRepository _holidayDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly HolidaysDataSeedContributor _holidaysDataSeedContributor;

        public HolidayDetailsDataSeedContributor(IHolidayDetailRepository holidayDetailRepository, IUnitOfWorkManager unitOfWorkManager, HolidaysDataSeedContributor holidaysDataSeedContributor)
        {
            _holidayDetailRepository = holidayDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _holidaysDataSeedContributor = holidaysDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _holidaysDataSeedContributor.SeedAsync(context);

            await _holidayDetailRepository.InsertAsync(new HolidayDetail
            (
                id: Guid.Parse("99bdfcdc-9dc9-4787-b12a-c845e47eb8a4"),
                startDate: new DateTime(2011, 1, 23),
                endDate: new DateTime(2017, 7, 20),
                description: "11a904b723024fb5a815e6",
                holidayId: Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756")
            ));

            await _holidayDetailRepository.InsertAsync(new HolidayDetail
            (
                id: Guid.Parse("017061bc-8b2e-49c5-9442-4fd04573c3ef"),
                startDate: new DateTime(2017, 11, 13),
                endDate: new DateTime(2010, 8, 14),
                description: "02391fc4c00e4cc38abd4163184066dc7e",
                holidayId: Guid.Parse("28d9ba00-744d-4d08-98f9-9176190c3756")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}