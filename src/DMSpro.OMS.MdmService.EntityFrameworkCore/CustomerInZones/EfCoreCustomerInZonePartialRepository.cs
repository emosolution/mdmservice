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

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial class EfCoreCustomerInZoneRepository
    {
        public virtual async Task<IQueryable<CustomerInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync()
        {
            return await GetQueryForNavigationPropertiesAsync();
        }
    }
}