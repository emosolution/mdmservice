using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute, Guid>
    {
        Task<List<ProductAttribute>> GetListAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isProductCategory = null,
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
            bool? isProductCategory = null,
            CancellationToken cancellationToken = default);
        
        Task<bool> CreateWithExcepAsync(List<ProductAttribute> seedData);
    }
}