using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.WeightMeasurements;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWeightMeasurementRepository _weightMeasurementRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WeightMeasurementsDataSeedContributor(IWeightMeasurementRepository weightMeasurementRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _weightMeasurementRepository = weightMeasurementRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _weightMeasurementRepository.InsertAsync(new WeightMeasurement
            (
                id: Guid.Parse("89509369-e333-4c67-9869-c40b3d1481bb"),
                code: "cd5c15d8ddf94ce3b45",
                name: "9e08a98396fd460b9939e975606c5465cc352d31c63d41968f",
                value: 1034472492
            ));

            await _weightMeasurementRepository.InsertAsync(new WeightMeasurement
            (
                id: Guid.Parse("d7d80755-df0f-49c8-bfd4-ddc9a353faa5"),
                code: "7dfb3bb1bf8b4697",
                name: "bff152df46684112bd2e3810be181037b640c3c2a9894ce8b1",
                value: 9041131
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}