using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.DimensionMeasurements;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDimensionMeasurementRepository _dimensionMeasurementRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DimensionMeasurementsDataSeedContributor(IDimensionMeasurementRepository dimensionMeasurementRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _dimensionMeasurementRepository = dimensionMeasurementRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _dimensionMeasurementRepository.InsertAsync(new DimensionMeasurement
            (
                id: Guid.Parse("fbf48fa3-c0bc-4fe6-bb35-f3fa6fa3207f"),
                code: "6d781fdd16474697a99a",
                name: "80d8ca9c9c804119aca74a70556b79155e93085e4434413b92",
                value: 155829626
            ));

            await _dimensionMeasurementRepository.InsertAsync(new DimensionMeasurement
            (
                id: Guid.Parse("0e69697f-172d-4b28-9d6e-4bc4396613fd"),
                code: "92c6345a6f2e4d53808c",
                name: "b4c6523644be4b0983b0e3ceaad30d9f537a96f38103422589",
                value: 1418040847
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}