using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public interface ISystemConfigRepository : IRepository<SystemConfig, Guid>
    {
        Task<List<SystemConfig>> GetListAsync(
            string filterText = null,
            string code = null,
            string description = null,
            string value = null,
            string defaultValue = null,
            bool? editableByTenant = null,
            ControlType? controlType = null,
            string dataSource = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string description = null,
            string value = null,
            string defaultValue = null,
            bool? editableByTenant = null,
            ControlType? controlType = null,
            string dataSource = null,
            CancellationToken cancellationToken = default);
    }
}