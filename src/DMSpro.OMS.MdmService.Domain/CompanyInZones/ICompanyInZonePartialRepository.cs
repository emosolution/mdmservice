using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using System.Linq;
namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial interface ICompanyInZoneRepository{
        Task<IQueryable<CompanyInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync();
		Task<List<CompanyInZone>> GetByIdAsync(List<Guid> ids);
    }
}
