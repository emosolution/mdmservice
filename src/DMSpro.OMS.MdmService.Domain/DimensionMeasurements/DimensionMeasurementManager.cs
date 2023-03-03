using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementManager : DomainService
    {
        private readonly IDimensionMeasurementRepository _dimensionMeasurementRepository;

        public DimensionMeasurementManager(IDimensionMeasurementRepository dimensionMeasurementRepository)
        {
            _dimensionMeasurementRepository = dimensionMeasurementRepository;
        }

        public async Task<DimensionMeasurement> CreateAsync(
        string code, string name, decimal value)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DimensionMeasurementConsts.CodeMaxLength, DimensionMeasurementConsts.CodeMinLength);
            Check.Length(name, nameof(name), DimensionMeasurementConsts.NameMaxLength);

            var dimensionMeasurement = new DimensionMeasurement(
             GuidGenerator.Create(),
             code, name, value
             );

            return await _dimensionMeasurementRepository.InsertAsync(dimensionMeasurement);
        }

        public async Task<DimensionMeasurement> UpdateAsync(
            Guid id,
            string code, string name, decimal value, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), DimensionMeasurementConsts.CodeMaxLength, DimensionMeasurementConsts.CodeMinLength);
            Check.Length(name, nameof(name), DimensionMeasurementConsts.NameMaxLength);

            var dimensionMeasurement = await _dimensionMeasurementRepository.GetAsync(id);

            dimensionMeasurement.Code = code;
            dimensionMeasurement.Name = name;
            dimensionMeasurement.Value = value;

            dimensionMeasurement.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dimensionMeasurementRepository.UpdateAsync(dimensionMeasurement);
        }

    }
}