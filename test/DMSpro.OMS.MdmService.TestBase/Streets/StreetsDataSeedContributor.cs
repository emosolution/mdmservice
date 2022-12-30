using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Streets;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IStreetRepository _streetRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StreetsDataSeedContributor(IStreetRepository streetRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _streetRepository = streetRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _streetRepository.InsertAsync(new Street
            (
                id: Guid.Parse("9bec2f73-3d23-4caf-bdcb-40e30cacece0"),
                name: "ZQ}qYgszus|kl~qss^qGo4jqTpG|9us"
            ));

            await _streetRepository.InsertAsync(new Street
            (
                id: Guid.Parse("69e36f74-b7e3-47d7-9f72-ca0b39bdef33"),
                name: "rv}uy[j|mspum}s^uuqmswi"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}