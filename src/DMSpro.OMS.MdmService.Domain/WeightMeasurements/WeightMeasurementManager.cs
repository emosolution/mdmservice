using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementManager : DomainService
    {
        private readonly IWeightMeasurementRepository _weightMeasurementRepository;

        public WeightMeasurementManager(IWeightMeasurementRepository weightMeasurementRepository)
        {
            _weightMeasurementRepository = weightMeasurementRepository;
        }

        public async Task<WeightMeasurement> CreateAsync(
        string code, string name, uint value)
        {
            var weightMeasurement = new WeightMeasurement(
             GuidGenerator.Create(),
             code, name, value
             );

            return await _weightMeasurementRepository.InsertAsync(weightMeasurement);
        }

        public async Task<WeightMeasurement> UpdateAsync(
            Guid id,
            string code, string name, uint value, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _weightMeasurementRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var weightMeasurement = await AsyncExecuter.FirstOrDefaultAsync(query);

            weightMeasurement.Code = code;
            weightMeasurement.Name = name;
            weightMeasurement.Value = value;

            weightMeasurement.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _weightMeasurementRepository.UpdateAsync(weightMeasurement);
        }

    }
}