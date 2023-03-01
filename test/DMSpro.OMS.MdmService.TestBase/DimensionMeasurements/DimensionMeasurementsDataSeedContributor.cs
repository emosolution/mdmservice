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
                id: Guid.Parse("b8aa42a1-51ea-48a8-8799-20f23ea90cac"),
                code: "82df60d9aab747d2ac77",
                name: "c12281cf94f3461a9b6978068139c4aad54bc18cbce14a2683",
                value: 1478350692
            ));

            await _dimensionMeasurementRepository.InsertAsync(new DimensionMeasurement
            (
                id: Guid.Parse("5223866f-e112-47bd-87a1-6c65b13f8934"),
                code: "337cc6bfcbf842b6aeda",
                name: "8ee2a7e7f29e4de48fad4451a1a63ef8ded22eb2ac8e43888c",
                value: 340235937
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}