using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

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
        string code, string name, uint value)
        {
            var dimensionMeasurement = new DimensionMeasurement(
             GuidGenerator.Create(),
             code, name, value
             );

            return await _dimensionMeasurementRepository.InsertAsync(dimensionMeasurement);
        }

        public async Task<DimensionMeasurement> UpdateAsync(
            Guid id,
            string code, string name, uint value, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _dimensionMeasurementRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var dimensionMeasurement = await AsyncExecuter.FirstOrDefaultAsync(query);

            dimensionMeasurement.Code = code;
            dimensionMeasurement.Name = name;
            dimensionMeasurement.Value = value;

            dimensionMeasurement.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dimensionMeasurementRepository.UpdateAsync(dimensionMeasurement);
        }

    }
}