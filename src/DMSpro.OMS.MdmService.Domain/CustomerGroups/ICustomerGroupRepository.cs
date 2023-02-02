using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public partial interface ICustomerGroupRepository : IRepository<CustomerGroup, Guid>
    {
        Task<List<CustomerGroup>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Type? groupBy = null,
            Status? status = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Type? groupBy = null,
            Status? status = null,
            CancellationToken cancellationToken = default);
    }
}