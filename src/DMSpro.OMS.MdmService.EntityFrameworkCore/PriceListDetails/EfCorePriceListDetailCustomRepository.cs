using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public partial class EfCorePriceListDetailRepository : EfCoreRepository<MdmServiceDbContext, PriceListDetail, Guid>, IPriceListDetailRepository
    {
        public virtual async Task<IQueryable<PriceListDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync()
        {
            return await GetQueryForNavigationPropertiesAsync();
        }
    }
}


