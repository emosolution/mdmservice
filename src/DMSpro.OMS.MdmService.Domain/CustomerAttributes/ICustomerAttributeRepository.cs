using DMSpro.OMS.MdmService.ProductAttributes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public interface ICustomerAttributeRepository : IRepository<CustomerAttribute, Guid>
    {
        Task<List<CustomerAttribute>> GetListAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            CancellationToken cancellationToken = default);

        Task<bool> CreateWithExcepAsync(List<CustomerAttribute> seedData);
    }
}