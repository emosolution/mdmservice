using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDataManager : DomainService
    {
        private readonly ISystemDataRepository _systemDataRepository;

        public SystemDataManager(ISystemDataRepository systemDataRepository)
        {
            _systemDataRepository = systemDataRepository;
        }

        public async Task<SystemData> CreateAsync(
        string code, string valueCode, string valueName)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SystemDataConsts.CodeMaxLength, SystemDataConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(valueCode, nameof(valueCode));
            Check.Length(valueCode, nameof(valueCode), SystemDataConsts.ValueCodeMaxLength, SystemDataConsts.ValueCodeMinLength);
            Check.NotNullOrWhiteSpace(valueName, nameof(valueName));
            Check.Length(valueName, nameof(valueName), SystemDataConsts.ValueNameMaxLength, SystemDataConsts.ValueNameMinLength);

            var systemData = new SystemData(
             GuidGenerator.Create(),
             code, valueCode, valueName
             );

            return await _systemDataRepository.InsertAsync(systemData);
        }

        public async Task<SystemData> UpdateAsync(
            Guid id,
            string code, string valueCode, string valueName, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SystemDataConsts.CodeMaxLength, SystemDataConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(valueCode, nameof(valueCode));
            Check.Length(valueCode, nameof(valueCode), SystemDataConsts.ValueCodeMaxLength, SystemDataConsts.ValueCodeMinLength);
            Check.NotNullOrWhiteSpace(valueName, nameof(valueName));
            Check.Length(valueName, nameof(valueName), SystemDataConsts.ValueNameMaxLength, SystemDataConsts.ValueNameMinLength);

            var queryable = await _systemDataRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var systemData = await AsyncExecuter.FirstOrDefaultAsync(query);

            systemData.Code = code;
            systemData.ValueCode = valueCode;
            systemData.ValueName = valueName;

            systemData.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _systemDataRepository.UpdateAsync(systemData);
        }

    }
}