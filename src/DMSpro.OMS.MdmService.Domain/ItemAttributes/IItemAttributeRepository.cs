using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public interface IItemAttributeRepository : IRepository<ItemAttribute, Guid>
    {
        Task<List<ItemAttribute>> GetListAsync(
            string filterText = null,
            string attrNo = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isSellingCategory = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string attrNo = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isSellingCategory = null,
            CancellationToken cancellationToken = default);
    }
}