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

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial class EfCoreCompanyInZoneRepository{

        public virtual async Task<IQueryable<CompanyInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync()
        {
            return await GetQueryForNavigationPropertiesAsync();
        }
    }
}