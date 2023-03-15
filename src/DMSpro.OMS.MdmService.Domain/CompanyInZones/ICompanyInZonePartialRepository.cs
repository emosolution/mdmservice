using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public partial interface ICompanyInZoneRepository : IRepository<CompanyInZone, Guid>
    {
		Task<List<CompanyInZone>> GetByIdAsync(List<Guid> ids);
    }
}
