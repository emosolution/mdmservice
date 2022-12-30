using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigManager : DomainService
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public SystemConfigManager(ISystemConfigRepository systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository;
        }

        public async Task<SystemConfig> CreateAsync(
        string code, string description, string value, string defaultValue, bool editableByTenant, ControlType controlType, string dataSource)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SystemConfigConsts.CodeMaxLength, SystemConfigConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), SystemConfigConsts.DescriptionMaxLength, SystemConfigConsts.DescriptionMinLength);
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.Length(value, nameof(value), SystemConfigConsts.ValueMaxLength, SystemConfigConsts.ValueMinLength);
            Check.NotNullOrWhiteSpace(defaultValue, nameof(defaultValue));
            Check.Length(defaultValue, nameof(defaultValue), SystemConfigConsts.DefaultValueMaxLength, SystemConfigConsts.DefaultValueMinLength);
            Check.NotNull(controlType, nameof(controlType));

            var systemConfig = new SystemConfig(
             GuidGenerator.Create(),
             code, description, value, defaultValue, editableByTenant, controlType, dataSource
             );

            return await _systemConfigRepository.InsertAsync(systemConfig);
        }

        public async Task<SystemConfig> UpdateAsync(
            Guid id,
            string code, string description, string value, string defaultValue, bool editableByTenant, ControlType controlType, string dataSource, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SystemConfigConsts.CodeMaxLength, SystemConfigConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.Length(description, nameof(description), SystemConfigConsts.DescriptionMaxLength, SystemConfigConsts.DescriptionMinLength);
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.Length(value, nameof(value), SystemConfigConsts.ValueMaxLength, SystemConfigConsts.ValueMinLength);
            Check.NotNullOrWhiteSpace(defaultValue, nameof(defaultValue));
            Check.Length(defaultValue, nameof(defaultValue), SystemConfigConsts.DefaultValueMaxLength, SystemConfigConsts.DefaultValueMinLength);
            Check.NotNull(controlType, nameof(controlType));

            var queryable = await _systemConfigRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var systemConfig = await AsyncExecuter.FirstOrDefaultAsync(query);

            systemConfig.Code = code;
            systemConfig.Description = description;
            systemConfig.Value = value;
            systemConfig.DefaultValue = defaultValue;
            systemConfig.EditableByTenant = editableByTenant;
            systemConfig.ControlType = controlType;
            systemConfig.DataSource = dataSource;

            systemConfig.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _systemConfigRepository.UpdateAsync(systemConfig);
        }

    }
}