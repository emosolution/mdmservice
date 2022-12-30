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
                id: Guid.Parse("d5870506-875a-4861-9faf-442ee0bbffc0"),
                code: "c9ad8ccac4564097",
                name: "7ef9575d4b4d4d65bc22e9bf5ca83fb3a7f1562581964842be",
                value: 1320792159
            ));

            await _weightMeasurementRepository.InsertAsync(new WeightMeasurement
            (
                id: Guid.Parse("c34fb548-4635-4c85-8128-0f0e1a949356"),
                code: "2f682a80d2e74edea",
                name: "08464d3702d54cd9bfbe8737a129ec9fb557225dc1534b0db2",
                value: 907925606
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}